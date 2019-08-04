public class Calendar
{
    public int year = 2019;
    public int month = 1;
    public int day = 1;
    public int time = 0;
    public int dday = 0;

    public bool nextTime()
    {
        ++time;
        time = checkday(month, day, time);

        if (dday != 0 && time == 0)
            return true;

        return false;
    }
    

    private int checkday(int month, int day, int time)
    {
        if(time  == 24)
        {
            ++this.day;
            ++dday;
            day = checkMonth(month, day);
            time = 0;
        }
        return time;
    }

    private int checkMonth(int month, int day)
    {
        int _result = day;
        switch (month)
        {
            case 1:
                if (day > 31)
                {
                    _result = 1;
                    ++this.month;
                }
                break;
            case 2:
                if (day > 28)
                {
                    _result = 1;
                    ++this.month;
                }
                break;
            case 3:
                if (day > 31)
                {
                    _result = 1;
                    ++this.month;
                }
                break;
            case 4:
                if (day > 30)
                {
                    _result = 1;
                    ++this.month;
                }
                break;
            case 5:
                if (day > 31)
                {
                    _result = 1;
                    ++this.month;
                }
                break;
            case 6:
                if (day > 30)
                {
                    _result = 1;
                    ++this.month;
                }
                break;
            case 7:
                if (day > 31)
                {
                    _result = 1;
                    ++this.month;
                }
                break;
            case 8:
                if (day > 31)
                {
                    _result = 1;
                    ++this.month;
                }
                break;
            case 9:
                if (day > 30)
                {
                    _result = 1;
                    ++this.month;
                }
                break;
            case 10:
                if (day > 31)
                {
                    _result = 1;
                    ++this.month;
                }
                break;
            case 11:
                if (day > 30)
                {
                    _result = 1;
                    ++this.month;
                }
                break;
            case 12:
                if (day > 31)
                {
                    _result = 1;
                    this.month = 1;
                    ++this.year;
                }
                break;
        }
        return _result;
    }
}