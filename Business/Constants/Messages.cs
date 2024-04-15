using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants;

//sabit olduğu için ve newlememek için static yaptık

public static class Messages
{
    public static string ProductAdded = "Ürün Eklendi";
    public static string ProductNameInvalid = "Ürün ismi geçersiz";
    public static string MaintenanceTime="Sistem bakımda";
    public static string ProductsListed="Ürünler Listelendi";
    public static string CategroyIsMax = "Kategori sayısını aştınız";
    public static string ProductNameAlreadyExists = "Aynı ürün ismi var";
    public static string CategoryIsMax = "Kategori sayısı dolu";

    public static string? AuthorizationDenied="Yetkiniz yok";
    public static string UserRegistered="Kullanıcı kaydı tamam";
    public static string UserNotFound="Kullanıcı bulunamadı";
    public static string PasswordError="Şifre hatalı";
    public static string SuccessfulLogin="giriş başarılı";
    public static string UserAlreadyExists="kullanıcı var";
    public static string AccessTokenCreated="token oluşturuldu";
}
