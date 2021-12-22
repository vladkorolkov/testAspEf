namespace southSoundWebsite;

public static class OperationsDB
{
    public static void Create()
    {

    }

    public static List<Example> ReadFromDbAboutArtist(string artistName)
    {
        using (reportsContext db = new reportsContext())
        {
            var query = (from q in db.Examples
                        where q.Исполнитель == artistName
                        select q).ToList();
            return query;
        }
    }


    public static void Update()
    {

    }

    public static void Delete()
    {

    }
}

