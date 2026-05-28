public override void Enroll()
{
    base.Enroll();
    Console.WriteLine($"Міграційний статус: Громадянин країни {CountryOfOrigin}. Віза дійсна до {VisaExpirationDate.ToShortDateString()}.");
}
