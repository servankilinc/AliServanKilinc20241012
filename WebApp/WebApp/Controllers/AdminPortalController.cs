using Business.Abstract;
using Business.Concrete;
using Core.DataAccess.DynamicQueries;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using Model.Dtos.Account_;
using Model.Models.Account_;
using Model.Models.Transfer_;
using Model.Models.User_;
using WebApp.ViewModels;

namespace WebApp.Controllers;


[Authorize(Roles = "Admin")]
public class AdminPortalController : Controller
{
    private readonly ITransferService _transferService;
    private readonly IUserService _userService;
    private readonly IAccountService _accountService;
    private readonly IAccountTypeService _accountTypeService;
    private readonly ITransferTypeService _transferTypeService;

    public AdminPortalController(ITransferService transferService, IUserService userService, IAccountService accountService, ITransferTypeService transferTypeService, IAccountTypeService accountTypeService)
    {
        _transferService = transferService;
        _userService = userService;
        _accountService = accountService;
        _transferTypeService = transferTypeService;
        _accountTypeService = accountTypeService;
    }

    public IActionResult Index()
    {
        return View();
    }


    public async Task<IActionResult> AccountList()
    {
        var defaultFilterModel = new AccountListFilterModel()
        {
            PageIndex = 0
        };

        AccountListRequestModel requestModel = new()
        {
            PagingRequest = new()
            {
                Page = defaultFilterModel.PageIndex,
                PageSize = 2
            },
        };

        var accountListResponse = await _accountService.GetAllByPaginationAsync(requestModel);

        var userList = new List<SelectListItem>() { new SelectListItem("Seçiniz", "") };
        var userListResponse = await _userService.GetAllAsync(new CancellationToken());
        userList.AddRange(userListResponse.Select(u => new SelectListItem($"{u.FirstName} {u.LastName}", u.Id.ToString())));

        var accountTypeList = new List<SelectListItem>() { new SelectListItem("Seçiniz", "") };
        var accountTypeListResponse = await _accountTypeService.GetAccountTypeListAsync();
        accountTypeList.AddRange(accountTypeListResponse.Select(at => new SelectListItem(at.Name, at.Id.ToString())));


        VMAdminPortalAccountList viewModel = new()
        {
            AccountList = accountListResponse,
            FilterModel = defaultFilterModel,
            AccountTypeList = accountTypeList,
            UserList = userList
        };

        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> AccountList(AccountListFilterModel filterModel)
    {
        var dynamicFilter = new Filter()
        {
            Operator = "base",
            Logic = "and",
            Filters = new List<Filter>()
        };

        if (filterModel.AccountTypeId != default)
        {
            dynamicFilter.Filters.Add(new Filter()
            {
                Field = "AccountTypeId",
                Operator = "eq",
                Value = filterModel.AccountTypeId.ToString(),
            });
        }

        if (filterModel.UserId != default)
        {
            dynamicFilter.Filters.Add(new Filter()
            {
                Field = "UserId",
                Operator = "eq",
                Value = filterModel.UserId.ToString(),
            });
        }      
        
        if (filterModel.AccountNo != default)
        {
            dynamicFilter.Filters.Add(new Filter()
            {
                Field = "AccountNo",
                Operator = "contains",
                Value = filterModel.AccountNo,
            });
        }

        AccountListRequestModel requestModel = new()
        {
            PagingRequest = new()
            {
                Page = filterModel.PageIndex,
                PageSize = 2
            },
            Filter = dynamicFilter
        };

        var accountListResponse = await _accountService.GetAllByPaginationAsync(requestModel);

        var userList = new List<SelectListItem>() { new SelectListItem("Seçiniz", "") };
        var userListResponse = await _userService.GetAllAsync(new CancellationToken());
        userList.AddRange(userListResponse.Select(u => new SelectListItem($"{u.FirstName} {u.LastName}", u.Id.ToString())));

        var accountTypeList = new List<SelectListItem>() { new SelectListItem("Seçiniz", "") };
        var accountTypeListResponse = await _accountTypeService.GetAccountTypeListAsync();
        accountTypeList.AddRange(accountTypeListResponse.Select(at => new SelectListItem(at.Name, at.Id.ToString())));


        VMAdminPortalAccountList viewModel = new()
        {
            AccountList = accountListResponse,
            FilterModel = filterModel,
            AccountTypeList = accountTypeList,
            UserList = userList
        };

        return View(viewModel);
    }



    public async Task<IActionResult> UserList()
    {
        var defaultFilterModel = new UserListFilterModel()
        {
            PageIndex = 0
        };

        UserListRequestModel requestModel = new()
        {
            PagingRequest = new()
            {
                Page = defaultFilterModel.PageIndex,
                PageSize = 2
            },
        };

        var userListResponse = await _userService.GetAllByPaginationAsync(requestModel, new CancellationToken());

        VMAdminPortalUserList viewModel = new()
        {
            Users = userListResponse,
            FilterModel = defaultFilterModel,         
        };

        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> UserList(UserListFilterModel filterModel)
    {
        UserListRequestModel requestModel = new()
        {
            PagingRequest = new()
            {
                Page = filterModel.PageIndex,
                PageSize = 2
            },
        };

        var userListResponse = await _userService.GetAllByPaginationAsync(requestModel, new CancellationToken());

        VMAdminPortalUserList viewModel = new()
        {
            Users = userListResponse,
            FilterModel = filterModel,
        };

        return View(viewModel);
    }


    public async Task<IActionResult> TransferList()
    {
        var defaultFilterModel = new TransferListFilterModel()
        {
            EndDate = DateTime.Now,
            StartDate = DateTime.Now.AddMonths(-1),
            Status = true,
            PageIndex = 0
        };

        var dynamicFilter = new Filter()
        {
            Operator = "base",
            Logic = "and",
            Filters = new List<Filter>
            {
                new Filter()
                {
                    Field = "Status",
                    Operator = "eq",
                    Value = defaultFilterModel.Status.ToString().ToLower(),
                },
                new Filter()
                {
                    Field = "Date",
                    Operator = "gte",
                    Value = defaultFilterModel.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                },
                new Filter()
                {
                    Field = "Date",
                    Operator = "lte",
                    Value = defaultFilterModel.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                }
            }
        };

        var requestModel = new TransferListRequestModel()
        {
            PagingRequest = new()
            {
                Page = 0,
                PageSize = 5
            },
            Sort = new()
            {
                Field = "Date",
                Dir = "desc"
            },
            Filter = dynamicFilter
        };

        var transferListResponse = await _transferService.GetTransfers(requestModel, new CancellationToken());

        var userList = new List<SelectListItem>() { new SelectListItem("Seçiniz", "") };
        var userListResponse = await _userService.GetAllAsync(new CancellationToken());
        userList.AddRange(userListResponse.Select(u => new SelectListItem($"{u.FirstName} {u.LastName}", u.Id.ToString())));

        var transferTypeList = new List<SelectListItem>() { new SelectListItem("Seçiniz", "") };
        var transferTypeListResponse = await _transferTypeService.GetTransferTypeListAsync(new CancellationToken());
        transferTypeList.AddRange(transferTypeListResponse.Select(tt => new SelectListItem(tt.Name, tt.Id.ToString())));

        var accountList = new List<SelectListItem>() { new SelectListItem("Seçiniz", "") };
        var accountListResponse = await _accountService.GetAllAsync(new CancellationToken());
        accountList.AddRange(accountListResponse.Select(a => new SelectListItem(a.AccountNo, a.Id.ToString()))); 
        
        VMAdminPortalTransferList viewModel = new() 
        {
            Transfers = transferListResponse,
            UserList = userList,
            AccountList = accountList,
            TransferTypeList = transferTypeList,
            FilterModel = defaultFilterModel,
        };

        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> TransferList(TransferListFilterModel filterModel)
    {
        ModelState.Clear();
        var dynamicFilter = new Filter()
        {
            Operator = "base",
            Logic = "and",
            Filters = new List<Filter>
            {
                new Filter()
                {
                    Field = "Status",
                    Operator = "eq",
                    Value = filterModel.Status.ToString().ToLower(),
                },
                new Filter()
                {
                    Field = "Date",
                    Operator = "gte",
                    Value = filterModel.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                },
                new Filter()
                {
                    Field = "Date",
                    Operator = "lte",
                    Value = filterModel.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                }
            }
        };

        // TransferType Base filter control
        if (filterModel.TransferTypeId != default)
        {
            dynamicFilter.Filters.Add(new Filter()
            {
                Field = "TransferTypeId",
                Operator = "eq",
                Value = filterModel.TransferTypeId.ToString(),
            });
        }


        // User Base filter control
        if (filterModel.UserId != default)
        {
            dynamicFilter.Filters.Add(new Filter()
            {
                Operator = "base",
                Logic = "or",
                Filters = new List<Filter>
                    {
                        new Filter()
                        {
                            Field = "SenderUserId",
                            Operator = "eq",
                            Value = filterModel.UserId.ToString(),
                        },
                        new Filter()
                        {
                            Field = "RecipientUserId",
                            Operator = "eq",
                            Value = filterModel.UserId.ToString(),
                        }
                    }
            });
        }

        // Account Number Base filter control
        if (filterModel.AccountId != default)
        {
            var accountResponseModel = await _accountService.GetAccountAsync(filterModel.AccountId);
            if (accountResponseModel != null)
            {
                dynamicFilter.Filters.Add(new Filter()
                {
                    Operator = "base",
                    Logic = "or",
                    Filters = new List<Filter>
                    {
                        new Filter()
                        {
                            Field = "SenderAccountId",
                            Operator = "eq",
                            Value = accountResponseModel.Id.ToString(),
                        },
                        new Filter()
                        {
                            Field = "RecipientAccountId",
                            Operator = "eq",
                            Value = accountResponseModel.Id.ToString(),
                        }
                    }
                });
            }
        }

        var requestModel = new TransferListRequestModel()
        {
            PagingRequest = new()
            {
                Page = filterModel.PageIndex,
                PageSize = 5
            },
            Sort = new()
            {
                Field = "Date",
                Dir = "desc"
            },
            Filter = dynamicFilter
        };

        var transferListResponse = await _transferService.GetTransfers(requestModel, new CancellationToken());

        var userList = new List<SelectListItem>() { new SelectListItem("Seçiniz", "") };
        var userListResponse = await _userService.GetAllAsync(new CancellationToken());
        userList.AddRange(userListResponse.Select(u => new SelectListItem($"{u.FirstName} {u.LastName}", u.Id.ToString())));

        var transferTypeList = new List<SelectListItem>() { new SelectListItem("Seçiniz", "") };
        var transferTypeListResponse = await _transferTypeService.GetTransferTypeListAsync(new CancellationToken());
        transferTypeList.AddRange(transferTypeListResponse.Select(tt => new SelectListItem(tt.Name, tt.Id.ToString())));

        var accountList = new List<SelectListItem>() { new SelectListItem("Seçiniz", "") };
        var accountListResponse = await _accountService.GetAllAsync(new CancellationToken());
        accountList.AddRange(accountListResponse.Select(a => new SelectListItem(a.AccountNo, a.Id.ToString())));

        VMAdminPortalTransferList viewModel = new()
        {
            Transfers = transferListResponse,
            UserList = userList,
            AccountList = accountList,
            TransferTypeList = transferTypeList,
            FilterModel = filterModel,
        };

        return View(viewModel);
    }
}
