using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project1

{
    class Program
    {
        static List<User> users = new List<User>();
        static User loggedInUser = null;
        static Dictionary<string, string> workoutPlans = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            InitializeWorkoutPlans();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Fitness App");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterUser();
                        break;
                    case "2":
                        LoginUser();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        static void InitializeWorkoutPlans()
        {
            workoutPlans.Add("Body Building", "Day 1: Push Workout + Cardio\nDay 2: Pull Workout + Cardio\nDay 3: Rest or Active Recovery...");
            workoutPlans.Add("Weight Loss", "Day 1: Cardio + Core Workout\nDay 2: HIIT + Strength Training\nDay 3: Rest or Active Recovery...");
            workoutPlans.Add("Weight Gain", "Day 1: Strength Training - Upper Body\nDay 2: Strength Training - Lower Body\nDay 3: Rest or Active Recovery...");
        }

        static void RegisterUser()
        {
            Console.Clear();
            Console.WriteLine("User Registration");

            string email;
            do
            {
                Console.Write("Enter your email: ");
                email = Console.ReadLine();
            } while (!IsValidEmail(email));

            string phone;
            do
            {
                Console.Write("Enter your phone number (10 digits): ");
                phone = Console.ReadLine();
            } while (!IsValidPhoneNumber(phone));

            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            int age = 0;
            while (true)
            {
                Console.Write("Enter your age: ");
                try
                {
                    age = int.Parse(Console.ReadLine());
                    if (age <= 0) throw new Exception("Age must be greater than 0.");
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for age.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            float weight = 0;
            while (true)
            {
                Console.Write("Enter your weight: ");
                try
                {
                    weight = float.Parse(Console.ReadLine());
                    if (weight <= 0) throw new Exception("Weight must be greater than 0.");
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for weight.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            float height = 0;
            while (true)
            {
                Console.Write("Enter your height: ");
                try
                {
                    height = float.Parse(Console.ReadLine());
                    if (height <= 0) throw new Exception("Height must be greater than 0.");
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for height.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.Write("Enter your medical aid (optional, leave blank if none): ");
            string medicalAid = Console.ReadLine();

            Console.Write("Enter your gym membership (optional, leave blank if none): ");
            string gym = Console.ReadLine();

            string password;
            string confirmPassword;
            do
            {
                Console.Write("Create a password: ");
                password = Console.ReadLine();
                Console.Write("Confirm your password: ");
                confirmPassword = Console.ReadLine();

                if (password != confirmPassword)
                {
                    Console.WriteLine("Passwords do not match. Please try again.");
                }
            } while (password != confirmPassword);

            string userId = name.Replace(" ", "").ToLower() + new Random().Next(1000, 9999).ToString();
            users.Add(new User(userId, email, phone, name, age, weight, height, medicalAid, gym, password));

            Console.WriteLine($"Registration successful! Your user ID is {userId}");
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
        }


        static void LoginUser()
        {
            Console.Clear();
            Console.WriteLine("User Login");

            Console.Write("Enter your user ID: ");
            string userId = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            foreach (User user in users)
            {
                if (user.UserId == userId && user.Password == password)
                {
                    loggedInUser = user;
                    Console.WriteLine("Login successful!");
                    UserDashboard();
                    return;
                }
            }

            Console.WriteLine("Incorrect user ID or password. Please try again.");
            Console.ReadKey();
        }

        static void UserDashboard()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome, {loggedInUser.Name}!");
                Console.WriteLine("1. View User Information");
                Console.WriteLine("2. Input Fitness Goal");
                Console.WriteLine("3. Log Workout");
                Console.WriteLine("4. Display Logged Workouts");
                Console.WriteLine("5. Display Fitness Goals");
                Console.WriteLine("6. View Notifications");
                Console.WriteLine("7. View Progress");
                Console.WriteLine("8. View Workout Plans");
                Console.WriteLine("9. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewUserInfo();
                        break;
                    case "2":
                        InputFitnessGoal();
                        break;
                    case "3":
                        LogWorkout();
                        break;
                    case "4":
                        ViewLoggedWorkouts();
                        break;
                    case "5":
                        DisplayFitnessGoals();
                        break;
                    case "6":
                        NotificationsPage();
                        break;
                    case "7":
                        ViewProgress();
                        break;
                    case "8":
                        WorkoutPlans();
                        break;
                    case "9":
                        loggedInUser = null;
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        static void WorkoutPlans()
        {
            Console.WriteLine("Body Building:");
            Console.WriteLine("Day 1: Push Workout + Cardio\nDay 2: Pull Workout + Cardio\nDay 3: Rest or Active Recovery...");

            Console.WriteLine("Weight Loss:");
            Console.WriteLine("Day 1: Cardio + Core Workout\nDay 2: HIIT + Strength Training\nDay 3: Rest or Active Recovery...");

            Console.WriteLine("Weight Gain:");
            Console.WriteLine("Day 1: Strength Training - Upper Body\nDay 2: Strength Training - Lower Body\nDay 3: Rest or Active Recovery...");

            Console.WriteLine("6-Week Strength Workout Plan:");
            Console.WriteLine(@"
Week 1-3
Day 1 – Push (Upper Body Strength)
• Workouts:
  o Push-ups: 3 sets x 12 reps
  o Dumbbell Bench Press: 3 sets x 10 reps
  o Tricep Dips: 3 sets x 12 reps
  o Overhead Dumbbell Press: 3 sets x 10 reps
• Calories Burned: ~200 cal

Day 2 – Pull (Upper Body Strength)
• Workouts:
  o Pull-ups or Assisted Pull-ups: 3 sets x 8 reps
  o Bent-Over Rows: 3 sets x 10 reps
  o Bicep Curls: 3 sets x 12 reps
  o Lat Pulldown: 3 sets x 10 reps
• Calories Burned: ~220 cal

Day 3 – Legs (Lower Body Strength)
• Workouts:
  o Squats: 4 sets x 10 reps
  o Leg Press: 3 sets x 12 reps
  o Lunges: 3 sets x 10 reps (each leg)
  o Calf Raises: 3 sets x 15 reps
• Calories Burned: ~250 cal

Day 4 – Core and Shoulders
• Workouts:
  o Plank: 3 sets x 30-60 seconds
  o Russian Twists: 3 sets x 15 reps (each side)
  o Lateral Raises: 3 sets x 12 reps
  o Shoulder Press: 3 sets x 10 reps
• Calories Burned: ~200 cal

Day 5 – Full Body Circuit
• Workouts:
  o Deadlifts: 3 sets x 8 reps
  o Burpees: 3 sets x 10 reps
  o Dumbbell Snatch: 3 sets x 10 reps (each arm)
  o Kettlebell Swings: 3 sets x 15 reps
• Calories Burned: ~300 cal

Day 6 – Rest (Active Recovery)
• Activities: Light Stretching, Yoga, or Foam Rolling
• Calories Burned: ~100 cal (if performing light activities)

Day 7 – Rest
• Calories Burned: 0 cal

Week 4-6
• Follow the same structure as Week 1-3 but increase reps or sets by 10-20% for progressive overload.
• Continue increasing intensity or weight to maintain muscle growth and strength gains.");

            Console.WriteLine("6-Week Cardio Workout Plan:");
            Console.WriteLine(@"
Week 1-3
Day 1 – Steady-State Cardio
• Activity: 30 minutes of jogging or brisk walking
• Calories Burned: ~250 cal

Day 2 – HIIT (High-Intensity Interval Training)
• Activity:
  o 5-minute warm-up (light jogging)
  o 30 seconds sprint / 30 seconds rest (repeat for 20 minutes)
  o 5-minute cool-down (walking)
• Calories Burned: ~400 cal

Day 3 – Rest or Light Cardio
• Activities: 20 minutes of light cycling or walking
• Calories Burned: ~150 cal

Day 4 – Circuit Training Cardio
• Activity:
  o Jump Rope: 1 minute
  o Jumping Jacks: 1 minute
  o High Knees: 1 minute
  o Mountain Climbers: 1 minute
  o Rest: 1 minute
  o Repeat the circuit 4 times
• Calories Burned: ~350 cal

Day 5 – Long-Distance Cardio
• Activity: 45-60 minutes of moderate-paced cycling or swimming
• Calories Burned: ~500 cal

Day 6 – Rest (Active Recovery)
• Activities: Light Stretching, Yoga, or Leisure Walk
• Calories Burned: ~100 cal (if performing light activities)

Day 7 – Rest
• Calories Burned: 0 cal

Week 4-6
• Follow the same structure as Week 1-3 but increase duration or intensity for each workout.
• Continue increasing intensity or mix in new cardio exercises (e.g., rowing, hiking) to keep it challenging.");

            Console.WriteLine("Press any key to return to the dashboard.");
            Console.ReadKey();
        }


        static void ViewUserInfo()
        {
            Console.Clear();
            Console.WriteLine("User Information");
            Console.WriteLine($"Name: {loggedInUser.Name}");
            Console.WriteLine($"Age: {loggedInUser.Age}");
            Console.WriteLine($"Weight: {loggedInUser.Weight}");
            Console.WriteLine($"Height: {loggedInUser.Height}");
            Console.WriteLine($"Medical Aid: {loggedInUser.MedicalAid}");
            Console.WriteLine($"Gym Membership: {loggedInUser.Gym}");
            Console.WriteLine("Press any key to return to the dashboard.");
            Console.ReadKey();
        }

        static void InputFitnessGoal()
        {
            Console.Clear();
            Console.WriteLine("Input Fitness Goal");

            Console.Write("Enter your fitness goal: ");
            string goal = Console.ReadLine();

            loggedInUser.FitnessGoals.Add(goal);

            Console.WriteLine("Fitness goal added successfully!");
            Console.WriteLine("Press any key to return to the dashboard.");
            Console.ReadKey();
        }

        static void LogWorkout()
        {
            Console.Clear();
            Console.WriteLine("Log Workout");

            Console.Write("Enter workout type (Strength/Cardio): ");
            string workoutType = Console.ReadLine().ToLower();

            IWorkout workout;
            switch (workoutType)
            {
                case "strength":
                    workout = LogStrengthWorkout();
                    break;
                case "cardio":
                    workout = LogCardioWorkout();
                    break;
                default:
                    Console.WriteLine("Invalid workout type. Please enter either 'Strength' or 'Cardio'.");
                    return;
            }

            loggedInUser.Workouts.Add(workout);
            Console.WriteLine("Workout logged successfully!");
            Console.WriteLine("Press any key to return to the dashboard.");
            Console.ReadKey();
        }

        static IWorkout LogStrengthWorkout()
        {
            StrengthWorkout workout = new StrengthWorkout();

            
            DateTime workoutDate;
            while (true)
            {
                Console.Write("Enter workout date (yyyy-MM-dd): ");
                string input = Console.ReadLine();

                if (DateTime.TryParseExact(input, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out workoutDate))
                {
                    workout.WorkoutDate = workoutDate;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please use yyyy-MM-dd format.");
                }
            }

           
            TimeSpan duration;
            while (true)
            {
                Console.Write("Enter duration (hh:mm:ss): ");
                string input = Console.ReadLine();

                if (TimeSpan.TryParse(input, out duration))
                {
                    workout.Duration = duration;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid time format. Please use hh:mm:ss format.");
                }
            }

            Console.Write("Enter number of sets: ");
            workout.Sets = int.Parse(Console.ReadLine());

            Console.Write("Enter number of reps per set: ");
            workout.Reps = int.Parse(Console.ReadLine());

            Console.Write("Enter weight lifted per rep (kg): ");
            workout.WeightLifted = double.Parse(Console.ReadLine());

            workout.CalculateStrengthProgress();
            return workout;
        }



        static IWorkout LogCardioWorkout()
        {
            CardioWorkout workout = new CardioWorkout();

            DateTime workoutDate;
            while (true)
            {
                Console.Write("Enter workout date (yyyy-mm-dd): ");
                string input = Console.ReadLine();

                if (DateTime.TryParseExact(input, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out workoutDate))
                {
                    workout.WorkoutDate = workoutDate;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please use yyyy-mm-dd format.");
                }
            }

            TimeSpan duration;
            while (true)
            {
                Console.Write("Enter duration (hh:mm:ss): ");
                string input = Console.ReadLine();

                if (TimeSpan.TryParse(input, out duration))
                {
                    workout.Duration = duration;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid time format. Please use hh:mm:ss format.");
                }
            }

            Console.Write("Enter distance covered (km): ");
            workout.Distance = double.Parse(Console.ReadLine());

            workout.CalculateCalories();
            return workout;
        }


        static void DisplayFitnessGoals()
        {
            Console.Clear();
            Console.WriteLine("Fitness Goals");
            if (loggedInUser.FitnessGoals.Count == 0)
            {
                Console.WriteLine("No fitness goals set.");
            }
            else
            {
                foreach (var goal in loggedInUser.FitnessGoals)
                {
                    Console.WriteLine($"- {goal}");
                }
            }
            Console.WriteLine("Press any key to return to the dashboard.");
            Console.ReadKey();
        }

        static void ViewLoggedWorkouts()
        {
            Console.Clear();
            Console.WriteLine("Logged Workouts");
            if (loggedInUser.Workouts.Count == 0)
            {
                Console.WriteLine("No workouts logged.");
            }
            else
            {
                foreach (var workout in loggedInUser.Workouts)
                {
                    workout.DisplayWorkoutDetails();
                    Console.WriteLine("--------------------");
                }
            }
            Console.WriteLine("Press any key to return to the dashboard.");
            Console.ReadKey();
        }

        static void NotificationsPage()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Notifications");

                int completedMilestones = loggedInUser.Workouts.Count / 7;
                double completionPercentage = (completedMilestones / 6.0) * 100;

                Console.WriteLine($"Milestone: {completedMilestones} reached");
                Console.WriteLine($"[{new string('/', completedMilestones)}{new string('.', 6 - completedMilestones)}] {completionPercentage:F1}%"); ;

                string reminderTime = "16:00";
                Console.WriteLine($"Training reminder: {reminderTime}");

                Console.WriteLine("1. Edit reminder");
                Console.WriteLine("2. Continue to menu");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        EditReminder();
                        break;
                    case "2":
                        return; 
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void EditReminder()
        {
            Console.Clear();
            Console.WriteLine("Edit Reminder");

            Console.Write("Enter new reminder time (HH:mm): ");
            string newTime = Console.ReadLine();

            if (TimeSpan.TryParse(newTime, out TimeSpan reminderTime))
            {
                Console.WriteLine($"Reminder updated to: {reminderTime}");
            }
            else
            {
                Console.WriteLine("Invalid time format. Please use HH:mm format.");
            }

            Console.WriteLine("Press any key to return to the notifications menu.");
            Console.ReadKey();
        }


        static void ViewProgress()
        {
            Console.Clear();
            Console.WriteLine("Progress Report");

            var strengthWorkouts = loggedInUser.Workouts.OfType<StrengthWorkout>().ToList();
            double averageWeightLifted = 0;
            if (strengthWorkouts.Count > 0)
            {
                double totalWeight = 0;
                foreach (var workout in strengthWorkouts)
                {
                    totalWeight += workout.WeightLifted;
                }
                averageWeightLifted = totalWeight / strengthWorkouts.Count;
            }
            Console.WriteLine($"Average Weight Lifted (kg): {averageWeightLifted:F2}");

            var cardioWorkouts = loggedInUser.Workouts.OfType<CardioWorkout>().ToList();
            double averageCaloriesBurned = 0;
            if (cardioWorkouts.Count > 0)
            {
                double totalCalories = 0;
                foreach (var workout in cardioWorkouts)
                {
                    totalCalories += workout.CaloriesBurned;
                }
                averageCaloriesBurned = totalCalories / cardioWorkouts.Count;
            }
            Console.WriteLine($"Average Calories Burned: {averageCaloriesBurned:F2}");

            int totalWorkouts = loggedInUser.Workouts.Count;
            Console.WriteLine($"Total Workouts Logged: {totalWorkouts} out of 42 days");

            Console.WriteLine("Press any key to return to the dashboard.");
            Console.ReadKey();
        }


        static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        static bool IsValidPhoneNumber(string phone)
        {
            return Regex.IsMatch(phone, @"^\d{10}$");
        }
    }

    interface IWorkout
    {
        DateTime WorkoutDate { get; set; }
        TimeSpan Duration { get; set; }

        void DisplayWorkoutDetails();
    }

    class StrengthWorkout : IWorkout
    {
        public DateTime WorkoutDate { get; set; }
        public TimeSpan Duration { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double WeightLifted { get; set; }

        public void DisplayWorkoutDetails()
        {
            Console.WriteLine($"Date: {WorkoutDate.ToShortDateString()} | Duration: {Duration} | Sets: {Sets} | Reps: {Reps} | Weight: {WeightLifted} kg");
        }

        public void CalculateStrengthProgress()
        {
        }
    }

    class CardioWorkout : IWorkout
    {
        public DateTime WorkoutDate { get; set; }
        public TimeSpan Duration { get; set; }
        public double Distance { get; set; }
        public double CaloriesBurned { get; set; }

        public void DisplayWorkoutDetails()
        {
            Console.WriteLine($"Date: {WorkoutDate.ToShortDateString()} | Duration: {Duration} | Distance: {Distance} km | Calories Burned: {CaloriesBurned}");
        }

        public void CalculateCalories()
        {
            CaloriesBurned = Distance * 60 + Duration.TotalMinutes * 10;
        }
    }

    class User
    {
        public string UserId { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public float Weight { get; private set; }
        public float Height { get; private set; }
        public string MedicalAid { get; private set; }
        public string Gym { get; private set; }
        public string Password { get; private set; }

        public List<IWorkout> Workouts { get; set; }
        public List<string> FitnessGoals { get; set; }
        public string CurrentPlan { get; set; }

        public User(string userId, string email, string phoneNumber, string name, int age, float weight, float height, string medicalAid, string gym, string password)
        {
            UserId = userId;
            Email = email;
            PhoneNumber = phoneNumber;
            Name = name;
            Age = age;
            Weight = weight;
            Height = height;
            MedicalAid = medicalAid;
            Gym = gym;
            Password = password;
            Workouts = new List<IWorkout>();
            FitnessGoals = new List<string>();
        }
    }
}