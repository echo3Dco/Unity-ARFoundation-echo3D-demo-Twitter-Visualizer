using System.Linq;

public class TwitterObject
{
    public TwitterObject(object egg)
    {
    }

    //Profile/Tweet creation date
    public string created_at;
    //Formated date time
    //public Twitter_DateTime FormatedDateTime;
    //The ID of the user of tweet
    public string id;
    //Language
    public string lang;

    /*public void FormatCreationTime()
    {
        char[] delim = { ' ', ':' };
        string[] chunks = created_at.Split(delim);

        FormatedDateTime.Weekday = chunks[0];
        FormatedDateTime.Month = chunks[1];
        FormatedDateTime.Day = int.Parse(chunks[2]);
        FormatedDateTime.Hour = int.Parse(chunks[3]);
        FormatedDateTime.Minute = int.Parse(chunks[4]);
        FormatedDateTime.Second = int.Parse(chunks[5]);
        FormatedDateTime.Offset = chunks[6];
        FormatedDateTime.Year = int.Parse(chunks[7]);
    }*/
}
