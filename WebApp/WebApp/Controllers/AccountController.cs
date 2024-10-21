using Business.Abstract;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Account_;
using Model.Models.Account_;
using System.Security.Claims;
using WebApp.Utils;
using WebApp.ViewModels;
using Core.DataAccess.DynamicQueries;

namespace WebApp.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IAccountTypeService _accountTypeService;
    private readonly ITransferService _transferService;
    private readonly IValidator<AccountCreateDto> _validatorAccountCreate;

    public AccountController(IAccountService accountService, IAccountTypeService accountTypeService, IValidator<AccountCreateDto> validatorAccountCreate, ITransferService transferService)
    {
        _accountService = accountService;
        _accountTypeService = accountTypeService;
        _validatorAccountCreate = validatorAccountCreate;
        _transferService = transferService;
    }

    public async Task<IActionResult> Index()
    {
        Claim? nameId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (nameId == null) return RedirectToAction("Login", "Auth");

        var userAccounts = await _accountService.GetUserAccountsAsync(Guid.Parse(nameId.Value), new CancellationToken());
        return View(userAccounts);
    }

    public async Task<IActionResult> UserAccounts(Guid userId)
    {
        ViewBag.UserId = userId;
        var userAccounts = await _accountService.GetUserAccountsAsync(userId, new CancellationToken());
        return View(userAccounts);
    }


    public async Task<IActionResult> CreateNewAccount()
    {

        var accountTypeList = await _accountTypeService.GetAccountTypeListAsync(new CancellationToken());

        ViewBag.SelectListAccountType = accountTypeList.Select(a => new SelectListItem(a.Name, a.Id.ToString()));

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewAccount(AccountCreateDto accountCreateModel)
    {
        Claim? nameId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (nameId == null) return RedirectToAction("Login", "Auth");

        accountCreateModel.UserId = Guid.Parse(nameId.Value);

        var validation = _validatorAccountCreate.Validate(accountCreateModel);
        if (!validation.IsValid)
        {
            validation.AddToModelState(ModelState);
            return View(accountCreateModel);
        }

        var response = await _accountService.CreateAccountAsync(accountCreateModel, new CancellationToken());

        return RedirectToAction("Index", "Account");
    }


    public async Task<IActionResult> History(Guid accountId)
    {
        var defaultFilterModel = new AccountHistoryFilterModel()
        {
            AccountId = accountId,
            EndDate = DateTime.Now,
            StartDate = DateTime.Now.AddMonths(-1),
            Status = true,
            PageIndex = 0
        };

        var requestModel = new AccountHistoryRequestModel()
        {
            AccountId = defaultFilterModel.AccountId,
            PagingRequest = new()
            {
                Page = 0,
                PageSize = 5
            }, 
            Filter = new Filter()
            {
                Operator= "base",
                Logic = "and",
                Filters = new List<Filter>
                {
                    new Filter()
                    {
                        Field  = "Status",
                        Operator = "eq",
                        Value = defaultFilterModel.Status.ToString().ToLower(),
                    },
                    new Filter()
                    {
                        Field  = "Date",
                        Operator = "gte",
                        Value = defaultFilterModel.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    },
                    new Filter()
                    {
                        Field  = "Date",
                        Operator = "lte",
                        Value = defaultFilterModel.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    }
                }
            }
        };

        var account = await _accountService.GetAccountAsync(accountId);
        var transferHistory = await _transferService.GetAccountHistoryAsync(requestModel, new CancellationToken());

        VMAccountHistory viewModel = new VMAccountHistory()
        {
            Account = account,
            Transfers = transferHistory,
            FilterModel = defaultFilterModel
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> History(AccountHistoryFilterModel filterModel)
    {
        var requestModel = new AccountHistoryRequestModel()
        {
            AccountId = filterModel.AccountId,
            PagingRequest = new()
            {
                Page = filterModel.PageIndex,
                PageSize = 5
            },
            Filter = new Filter()
            {
                Operator = "base",
                Logic = "and",
                Filters = new List<Filter>
                {
                    new Filter()
                    {
                        Field  = "Status",
                        Operator = "eq",
                        Value = filterModel.Status.ToString().ToLower(),
                    },
                    new Filter()
                    {
                        Field  = "Date",
                        Operator = "gte",
                        Value = filterModel.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    },
                    new Filter()
                    {
                        Field  = "Date",
                        Operator = "lte",
                        Value = filterModel.EndDate.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).ToString("yyyy-MM-ddTHH:mm:ss"),
                    }
                }
            }
        };


        var account = await _accountService.GetAccountAsync(filterModel.AccountId);
        var transferHistory = await _transferService.GetAccountHistoryAsync(requestModel, new CancellationToken());

        VMAccountHistory viewModel = new VMAccountHistory()
        {
            Account = account,
            Transfers = transferHistory,
            FilterModel = filterModel
        };

        return View(viewModel);
    }
}
