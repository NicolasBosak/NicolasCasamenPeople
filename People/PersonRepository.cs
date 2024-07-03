using SQLite;
using People.NCModels;
namespace People;

public class PersonRepository
{
    string _dbPath;

    public string StatusMessage { get; set; }

    // TODO: Add variable for the SQLite connection

    private SQLiteConnection conn;
    private void Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<NCPerson>();
    }

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;                        
    }

    public void AddNewPerson(string name)
    {            
        int result = 0;
        try
        {
            Init();

            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            // TODO: Insert the new person into the database
            result = conn.Insert(new NCPerson { Name = name });

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }

    }

    public List<NCPerson> GetAllPeople()
    {
        try
        {
            Init();
            return conn.Table<NCPerson>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<NCPerson>();
    }
}
