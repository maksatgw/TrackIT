using NToastNotify;

namespace TrackIT.UI.Extensions
{
    public static class ToastrExtension
    {
        public static void AddErrorToastMessageWithCustomTitle(this IToastNotification toastNotification, string message)
        {
            toastNotification.AddErrorToastMessage(message, new ToastrOptions { Title = "Hata" });
        }
        public static void AddSuccessToastMessageWithCustomTitle(this IToastNotification toastNotification, string message)
        {
            toastNotification.AddSuccessToastMessage(message, new ToastrOptions { Title = "Başarılı" });
        }
    }
}
