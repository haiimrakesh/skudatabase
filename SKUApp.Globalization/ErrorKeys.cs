namespace SKUApp.Globalization;

public static class ErrorKeys
{   
    public static string NameRequired { get; } = "InvalidEmail";
    public static string NameInvalidLength { get; } = "InvalidPassword";
    public const string InvalidPhoneNumber = "InvalidPhoneNumber";
    public const string InvalidUsername = "InvalidUsername";
    public const string InvalidDate = "InvalidDate";
    public const string InvalidTime = "InvalidTime";
    public const string InvalidDateTime = "InvalidDateTime";
    public const string InvalidUrl = "InvalidUrl";
    public const string InvalidCreditCardNumber = "InvalidCreditCardNumber";
    public const string InvalidPostalCode = "InvalidPostalCode";
    public const string InvalidSSN = "InvalidSSN";
}
