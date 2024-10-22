# Express Bilgisayar Case Study: Çok Katmanlı Mimari ile .NET Web API ve MVC Uygulaması

Bu proje, .NET teknolojileri kullanılarak geliştirilmiş çok katmanlı bir mimariye sahip farklı sistemlerle entegre olabilmesi için Api Web API ve Web uygulaması için MVC uygulamalarından oluşmaktadır. Proje, MSSQL veritabanı ile çalışmakta ve tüm veritabanı şeması ve hasır verileri ile repo'da yer almaktadır.
 

## Kullanılan Teknolojiler
* .NET core 8 Web API ve MVC projeleri
* Entity Framework Core – Veritabanı işlemleri için ORM
* Authentication ve Authorization – Api kullanımı için JWT token ve MVC Uygulaması için Cookie, Role bazlı yapılar kullanıldı
* MSSQL Server – Veritabanı yönetim sistemi
* FluentValidation – Validasyon işlemleri için
* AutoMapper – DTO ve entity dönüşümleri için
* Repository Pattern – Veri erişim katmanı için
* Javascript & Jquery - ajax, dom manipülasyonları vb. için
* HTML & Css & Bootstrap - Web MVC uygulaması arayüz tasarımlarında kullanıldı


## Proje Yapısı ve Katmanlar
#### 1. Core Layer:
Katmanların ortak kullanımında olan Global Exception yapıları, enums, sınıf imzaları vb. barındırmaktadır.

#### 2. Model Layer:
Veritabanı entity  sınıfları, Dto sınıfları ve request response modellerini barındırmaktadır.

#### 3. Data Access Layer:
Veritabanı işlemlerini soyutlayan Repository arayüzleri ve sınıfları bulunur. Veri tabanına erişim yönetilir.

#### 4. Business Layer:
İş kurallarını barındıran servislerin tanımlandığı katmandır. Cache veya Data Access katmanı gibi kaynak sağlayıcıları business katmanında kullanılır. API ve MVC uygulamaları, bu katmandaki servisleri kullanır.

#### 5. Web API:
RESTful API yapısında geliştirilmiş bir uygulamadır. API, JSON formatında veri döner ve JWT Token role bazlı authentication ve authorization destekler.

#### 6. MVC Uygulaması:
Kullanıcı arayüz'leri sunan katmandır. Çok katmanlı mimaride geliştirlmiştir.


## Kurulum
#### Ön Gereksinimler
- MSSQL Server ve SQL Server Management Studio (SSMS)
* .NET 8 SDK
#### Adım 1: Veritabanını Kurma
Proje reposundaki database.sql dosyasını bulun. Bu dosya, veritabanı ve gerekli tabloları verilerle birlikte oluşturur.

Veritabanı başarılı bir şekilde oluşturulduktan sonra, API ve MVC uygulamaları bu veritabanına bağlanacaktır.

#### Adım 2: API ve MVC Uygulamalarının Yapılandırılması
Veritabanı connection string bilgisi nedeniyle oluşabilecek hataların önüne geçmek için appsettings.json dosyasını hem Web API hem de MVC projelerinde bulup kontrol ediniz.

#### Adım 3: Test Verileri İle Kullanım
Hazır verilerde bulunan aşağıdaki kullanıcılar ile sistem test edilebil
* (Admin rolüne sahip) e posta adresi: serkan@mail.com, şifre: string 
* e posta adresi: merve@mail.com, şifre: string
* e posta adresi: ayse@mail.com, şifre: string 
* e posta adresi: mehmet@mail.com, şifre: string

## License

[MIT](https://choosealicense.com/licenses/mit/)