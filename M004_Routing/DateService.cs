namespace M004_Routing;

public class DateService
{
	public bool ShortDate = false;

	public DateTime GetCurrentDate()
	{
		//if (ShortDate)
		//	return new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
		return DateTime.Now;
	}
}
