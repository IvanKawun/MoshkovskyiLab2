namespace MoshkovskyiLab2.Models;
using System;
using System.Text;


class Person
{
    #region Fields
    private readonly string _sunSign;
    private readonly string _chineseSign;
    private readonly bool _isBirthday;
    private readonly bool _isAdult;

    public string _firstname;
    public string _lastname;
    public string _email;
    public DateTime _birthday;
    #endregion
    
    
    
    public Person(string firstName, string lastName, string email, DateTime birthDay)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        BirthDay = birthDay;
        
        IsAdult = Age() >= 18;
        SunSign = ZodiacHelper.GetWesternZodiac(BirthDay);
        ChineseSign = ZodiacHelper.GetChineseZodiac(BirthDay);
        IsBirthday = CheckIsBirthday();
        
    }
    
    public Person(string firstName, string lastName, string email)
        : this(firstName, lastName, email, DateTime.Today) { }

    public Person(string firstName, string lastName, DateTime birthDate)
        : this(firstName, lastName, "something@gmail.com", birthDate) { }
    
    # region Propreties
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDay { get; private set; }
    public bool IsAdult { get; private set; }
    public string SunSign { get; private set; }
    public string ChineseSign { get; private set; }
    public bool IsBirthday { get; private set; }
    #endregion

    private int Age()
    {
        var day = DateTime.Today;
        var age = day.Year - BirthDay.Year;

        if (BirthDay.Date > day.AddYears(age * (-1)))
        {
            age--;
        }

        return age;
    }

    public bool CheckCorrectBirthDay()
    {
        var age = Age();
        if (age < 0) return false;
        if (age > 135) return false;
        else return true;
    }

    private bool CheckIsBirthday()
    {
        var today = DateTime.Today;
        return BirthDay.Day == today.Day && BirthDay.Month == today.Month;
    }
    
}