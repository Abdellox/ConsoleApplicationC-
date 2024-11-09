//Job Portal Console Application:

using Newtonsoft.Json;

JobPortal portal = new JobPortal();
int choice;

do
{
    Console.WriteLine("Welcome to Job Finder");
    Console.WriteLine("1. Search for Jobs");
    Console.WriteLine("2. Post a Job");
    Console.WriteLine("3. View Job Listings");
    Console.WriteLine("4. Exit");
    Console.Write("Choose an option: ");
    choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
            portal.SearchJobs();
            break;
        case 2:
            portal.PostJob();
            break;
        case 3:
            portal.ViewJobListings();
            break;
        case 4:
            Console.WriteLine("Exiting the application...");
            break;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
} while (choice != 4);

//---------------------------------------------------------------------------------
public class Job
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Company { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }


    public override string ToString()
    {
        return $"Title: {Title}, Company: {Company}, Location: {Location}, Description: {Description}";
    }

}
//-------------------------------------------------------
class JobPortal
{
    private List<Job> jobs = new List<Job>();

    public void SearchJobs()
    {
        Console.Write("Enter job title or keyword to search: ");
        string? keyword = Console.ReadLine();

        // Check if keyword is null or empty
        if (string.IsNullOrWhiteSpace(keyword))
        {
            Console.WriteLine("Please enter a valid keyword.");
            return; // Exit the method early
        }

        // Convert to lowercase only if it's non-null
        string lowerKeyword = keyword.ToLower();

        // Perform the search

        var foundJobs = jobs.FindAll(job =>
            job.Title.Contains(lowerKeyword, StringComparison.CurrentCultureIgnoreCase)
        );


        // Check if any jobs were found
        if (foundJobs.Count > 0)
        {
            Console.WriteLine("Jobs found:");
            foreach (var job in foundJobs)
            {
                Console.WriteLine(job);
            }
        }
        else
        {
            Console.WriteLine("No jobs found with that keyword.");
        }
    }

    public void PostJob()
    {
        Job newJob = new Job();

        Console.Write("Enter job title: ");
        newJob.Title = Console.ReadLine();

        Console.Write("Enter company name: ");
        newJob.Company = Console.ReadLine();

        Console.Write("Enter job location: ");
        newJob.Location = Console.ReadLine();

        Console.Write("Enter job description: ");
        newJob.Description = Console.ReadLine();

        jobs.Add(newJob);
        Console.WriteLine("Job posted successfully!");
    }



private const string FilePath = "Jobs.json";

    public void ViewJobListings()
    {
        
        try
        {
            
            // Read the JSON file content
            var json = File.ReadAllText(FilePath);

            // Deserialize the JSON to a list of Job objects
            var jobListings = JsonConvert.DeserializeObject<List<Job>>(json);

            // Check if there are any job listings
            if (jobListings is null || jobListings.Count == 0)
            {
                Console.WriteLine("No job listings found.");
                return;
            }

            // Display the job listings
            foreach (var job in jobListings)
            {
                Console.WriteLine($"ID: {job.Id}");
                Console.WriteLine($"Title: {job.Title}");
                Console.WriteLine($"Company: {job.Company}");
                Console.WriteLine($"Location: {job.Location}");
                Console.WriteLine($"Description: {job.Description}");
                Console.WriteLine(new string('-', 20));
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("The jobs.json file was not found.");
        }
     
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
    }





