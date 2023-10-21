namespace E_Commerce_Platform.Contracts;

public static class AlertMessageTemplates
{
    public class Order
    {
        public const string TITLE = "Sifarisle bagli yenilik";

        public const string CREATED = "#{order_number} nömrəli yeni sifariş yaradıldı";

        public const string APPROVED = "Sizin #{order_number} təsdiqləndi";
        public const string REJECTED = "Sizin #{order_number}  təsdiqlənmədi.";
        public const string SENT = "Sizin #{order_number} göndərildi, kuryer sizinlə əlaqə saxlayacaq.";
        public const string COMPLETED = "Sizin #{order_number} kuryer tərəfindən təhvil verildi.";
    }

}
