using System;
using System.Collections.Generic;
using System.IO;

namespace AcademicInstitution
{

    class Person
    {
        private string Type;
        private string Surname;
        private string Forename;
        private DateTime DOB;
        protected bool Incomplete;
        protected List<string> IncompleteProperties = new List<string>();


        public Person()
        {
            Type = "Person";
            Console.WriteLine("Please enter Surname");
            Surname = Console.ReadLine();
            Console.WriteLine("Please enter Forename");
            Forename = Console.ReadLine();
            Console.WriteLine("Please enter date of birth in the format DD/MM/YYYY");
            DOB = Convert.ToDateTime(Console.ReadLine());
        }

        public Person(string[] SplitLine)
        {
            Type = SplitLine[0];
            Surname = SplitLine[1];
            Forename = SplitLine[2];
            DOB = Convert.ToDateTime(SplitLine[3]);
        }
        public Person(string surname)
        {
            Surname = surname;
        }

        public virtual void DisplayProperties()
        {
            Console.Write($"{Type}: Surname: {Surname}, Forename: {Forename}, DOB: {DOB.Date}");
        }

        public virtual void WriteProperties(StreamWriter File)
        {
            File.Write($"{Type},{Surname},{Forename},{DOB.Date}");
        }

        public virtual void Edit(string Response, ref List<Person> People)
        {
            switch (Response.ToUpper())
            {
                case "SURNAME":
                    Console.WriteLine("Please enter new Surname");
                    Surname = Console.ReadLine();
                    break;
                case "FORENAME":
                    Console.WriteLine("Please enter new Forename");
                    Forename = Console.ReadLine();
                    break;
                case "DOB":
                    Console.WriteLine("Please enter new date of birth in the format DD/MM/YYYY");
                    DOB = Convert.ToDateTime(Console.ReadLine());
                    break;
                default:
                    Console.WriteLine("Field not found.");
                    break;
            }
        }

        public void Complete(Person P, ref List<Person> People)
        {
            List<string> CompleteProperties = new List<string>();
            foreach(string Property in IncompleteProperties)
            {
                P.Edit(Property, ref People);
                CompleteProperties.Add(Property);
            }
            foreach(string Property in CompleteProperties)
            {
                IncompleteProperties.Remove(Property);
            }
            Incomplete = false;
        }

        public string ReturnType()
        {
            return Type;
        }
        public void SetType(string type)
        {
            Type = type;
        }

        public string GetSurname()
        {
            return Surname;
        }
        public void SetSurname(string surname)
        {
            Surname = surname;
        }

        public string GetForename()
        {
            return Forename;
        }
        public void SetForename(string forename)
        {
            Forename = forename;
        }

        public DateTime GetDOB()
        {
            return DOB;
        }
        public void SetDOB(DateTime date)
        {
            DOB = date;
        }

        public bool IsIncomplete()
        {
            return Incomplete;
        }

        public List<string> GetIncompleteProperties()
        {
            return IncompleteProperties;
        }
    }


    class Staff : Person
    {
        private string EmployeeNumber;
        private string NINumber;
        private string JobTitle;
        private bool Permanent;
        private string Role;

        public Staff()
        {
            SetType("Staff");
            Console.WriteLine("Please enter Employee ID Number");
            EmployeeNumber = Console.ReadLine();
            Console.WriteLine("Please enter Employee National Insurance Number");
            NINumber = Console.ReadLine();
            Console.WriteLine("Please enter Employee Job Title");
            JobTitle = Console.ReadLine();
            Console.WriteLine("Is this a permanent employee? Yes / No");
            if (Console.ReadLine().ToUpper() == "YES") {
                Permanent = true;
            } else {
                Permanent = false;
            }
            Console.WriteLine("What is this employee's role?");
            Role = Console.ReadLine();
        }
        public Staff(string[] SplitLine) : base(SplitLine)
        {
            EmployeeNumber = SplitLine[4];
            NINumber = SplitLine[5];
            JobTitle = SplitLine[6];
            Permanent = Convert.ToBoolean(SplitLine[7]);
            Role = SplitLine[8];
            
        }
        public Staff(string surname) : base(surname)
        {

        }

        public override void DisplayProperties()
        {
            base.DisplayProperties();
            Console.Write($", Employee Number: {EmployeeNumber}, NI Number: {NINumber}, Job Title: {JobTitle}, Is Permanent: {Permanent}, Role: {Role}");
        }

        public override void WriteProperties(StreamWriter File)
        {
            base.WriteProperties(File);
            File.Write($",{EmployeeNumber},{NINumber},{JobTitle},{Permanent},{Role}");
        }

        public override void Edit(string Response, ref List<Person> People)
        {
            switch (Response.ToUpper())
            {
                case "EMPLOYEENUMBER":
                    Console.WriteLine("Please enter new Employee Number");
                    EmployeeNumber = Console.ReadLine();
                    break;
                case "NINUMBER":
                    Console.WriteLine("Please enter new National Insurance Number");
                    NINumber = Console.ReadLine();
                    break;
                case "JOBTITLE":
                    Console.WriteLine("Please enter new Job Title");
                    JobTitle = Console.ReadLine();
                    break;
                case "PERMANENT":
                    Console.WriteLine("Is this employee permanent?");
                    if (Console.ReadLine().ToUpper() == "YES") {
                        Permanent = true;
                    } else {
                        Permanent = false;
                    }
                    break;
                case "ROLE":
                    Console.WriteLine("Please enter new Role");
                    Role = Console.ReadLine();
                    break;
                default:
                    base.Edit(Response, ref People);
                    break;
            }
        }

        public string GetEmployeeNumber()
        {
            return EmployeeNumber;
        }
        public void SetEmployeeNumber(string number)
        {
            EmployeeNumber = number;
        }

        public string GetNINumber()
        {
            return NINumber;
        }
        public void SetNINumber(string number)
        {
            NINumber = number;
        }

        public string GetJobTitle()
        {
            return JobTitle;
        }
        public void SetJobTitle(string title)
        {
            JobTitle = title;
        }

        public bool IsPermanent()
        {
            return Permanent;
        }
        public void SetPermanent(bool permanent)
        {
            Permanent = permanent;
        }

        public string GetRole()
        {
            return Role;
        }
        public void SetRole(string role)
        {
            Role = role;
        }

    } 

    class AcademicStaff : Staff
    {
        private string Department;
        private string Specialisation;
        

        public AcademicStaff()
        {
            SetType("AcademicStaff");
            Console.WriteLine($"What department does this {GetRole()} work in");
            Department = Console.ReadLine();
            Console.WriteLine($"What does this {GetRole()} specialise in?");
            Specialisation = Console.ReadLine();
        }
        public AcademicStaff(string[] SplitLine) : base(SplitLine)
        {
            Department = SplitLine[9];
            Specialisation = SplitLine[10];
            if (SplitLine[11] == "true")
            {
                foreach (string s in SplitLine[12].Split(';'))
                {
                    if (s != "")
                    {
                        IncompleteProperties.Add(s);
                    }
                }
            }
        }
        public AcademicStaff(string surname) : base(surname)
        {
            SetType("AcademicStaff");
            Incomplete = true;
            IncompleteProperties.Add("FORENAME");
            IncompleteProperties.Add("DOB");
            IncompleteProperties.Add("EMPLOYEENUMBER");
            IncompleteProperties.Add("NINUMBER");
            IncompleteProperties.Add("JOBTITLE");
            IncompleteProperties.Add("PERMANENT");
            IncompleteProperties.Add("ROLE");
            IncompleteProperties.Add("DEPARTMENT");
            IncompleteProperties.Add("SPECIALISATION");
        }

        public override void DisplayProperties()
        {
            base.DisplayProperties();
            Console.Write($", Department: {Department}, Specialistation: {Specialisation}");
        }

        public override void WriteProperties(StreamWriter File)
        {
            base.WriteProperties(File);
            File.Write($",{Department},{Specialisation},{IsIncomplete()}");
        }

        public override void Edit(string Response, ref List<Person> People)
        {
            switch (Response.ToUpper())
            {
                case "DEPARTMENT":
                    Console.WriteLine("Please enter new Department");
                    Department = Console.ReadLine();
                    break;
                case "SPECIALISATION":
                    Console.WriteLine("Please enter new Specialisation");
                    Specialisation = Console.ReadLine();
                    break;
                default:
                    base.Edit(Response, ref People);
                    break;
            }
        }

        public string GetDepartment()
        {
            return Department;
        }
        public void SetDepartment(string department)
        {
            Department = department;
        }

        public string GetSpecialisation()
        {
            return Specialisation;
        }
        public void SetSpecialisation(string specialisation)
        {
            Specialisation = specialisation;
        }

        

    } 

    class SupportStaff : Staff
    {
        private bool Salaried;
        private double HoursWorked;
        
        public SupportStaff()
        {
            SetType("SupportStaff");
            Console.WriteLine($"Is this {GetRole()} Salaried? Yes / No");
            if (Console.ReadLine().ToUpper() == "YES") {
                Salaried = true;
            } else {
                Salaried = false;
            }
            Console.WriteLine($"Please enter the amount of hours worked by this {GetRole()}");
            HoursWorked = Convert.ToDouble(Console.ReadLine());
        }
        public SupportStaff(string[] SplitLine) : base(SplitLine)
        {
            Salaried = Convert.ToBoolean(SplitLine[9]);
            HoursWorked = Convert.ToDouble(SplitLine[10]);
        }

        public override void DisplayProperties()
        {
            base.DisplayProperties();
            Console.Write($", Is Salaried: {Salaried}, Hours Worked: {HoursWorked}");
        }

        public override void WriteProperties(StreamWriter File)
        {
            base.WriteProperties(File);
            File.Write($",{Salaried},{HoursWorked}");
        }

        public override void Edit(string Response, ref List<Person> People)
        {
            switch (Response.ToUpper())
            {
                case "SALARIED":
                    Console.WriteLine("Is this employee salaried?");
                    if (Console.ReadLine().ToUpper() == "YES") {
                        Salaried = true;
                    } else {
                        Salaried = false;
                    }
                    break;
                case "HOURSWORKED":
                    Console.WriteLine("Please enter new Hours Worked");
                    HoursWorked  += Convert.ToDouble(Console.ReadLine());
                    break;
                default:
                    base.Edit(Response, ref People);
                    break;
            }
        }

        public bool IsSalaried()
        {
            return Salaried;
        }
        public void SetSalaried(bool salaried)
        {
            Salaried = salaried;
        }

        public double GetHoursWorked()
        {
            return HoursWorked;
        }
        public void SetHoursWorked(double hours)
        {
            HoursWorked = hours;
        }

        
    } 


    class Student : Person
    {
        private string StudentNumber;
        private DateTime DateJoined;
        private string Course;

        public Student()
        {
            SetType("Student");
            Console.WriteLine("Please enter this student's ID Number");
            StudentNumber = Console.ReadLine();
            Console.WriteLine("Please enter the date this student joined the academic establishment in the formate DD/MM/YYYY");
            DateJoined = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("What is this student's course?");
            Course = Console.ReadLine();
        }
        public Student(string[] SplitLine) : base(SplitLine)
        {
            StudentNumber = SplitLine[4];
            DateJoined = Convert.ToDateTime(SplitLine[5]);
            Course = SplitLine[6];
        }

        public override void DisplayProperties()
        {
            base.DisplayProperties();
            Console.Write($", Student Number: {StudentNumber}, Date Joined: {DateJoined}, Course: {Course}");
        }

        public override void WriteProperties(StreamWriter File)
        {
            base.WriteProperties(File);
            File.Write($",{StudentNumber},{DateJoined},{Course}");
        }

        public override void Edit(string Response, ref List<Person> People)
        {
            switch (Response.ToUpper())
            {
                case "STUDENTNUMBER":
                    Console.WriteLine("Please enter new Student Number");
                    StudentNumber = Console.ReadLine();
                    break;
                case "DATEJOINED":
                    Console.WriteLine("Please enter new date joined in the format DD/MM/YYYY");
                    DateJoined = Convert.ToDateTime(Console.ReadLine());
                    break;
                case "COURSE":
                    Console.WriteLine("Please enter new Course");
                    Course = Console.ReadLine();
                    break;
                default:
                    base.Edit(Response, ref People);
                    break;
            }
        }

        public string GetStudentNumber()
        {
            return StudentNumber;
        }
        public void SetStudentNumber(string number)
        {
            StudentNumber = number;
        }

        public DateTime GetDateJoined()
        {
            return DateJoined;
        }
        public void SetDateJoined(DateTime date)
        {
            DateJoined = date;
        }

        public string GetCourse()
        {
            return Course;
        }
        public void SetCourse(string course)
        {
            Course = course;
        }

    } 
    
    class Undergraduate : Student
    {
        private AcademicStaff Tutor;
        private char PredictedGrade;
        private string Accommodation;

        public Undergraduate(ref List<Person> People)
        {
            SetType("Undergraduate");
            Console.WriteLine("Please enter the surname of this student's tutor");
            string SurnameToFind = Console.ReadLine();
            bool Found = false;
            foreach(Person StaffMember in People)
            {
                if(StaffMember.GetType().Name == "AcademicStaff")
                {
                    if (StaffMember.GetSurname() == SurnameToFind)
                    {
                        Console.WriteLine("Found tutor!");
                        Tutor = (AcademicStaff)StaffMember;
                        Found = true;
                        break;
                    }
                }
            }
            if (!Found)
            {
                Console.WriteLine("Tutor not found. Creating new tutor on system.");
                AcademicStaff TempTutor = new AcademicStaff(SurnameToFind);
                People.Add(TempTutor);
                Tutor = TempTutor;
            }
            Console.WriteLine("What is this student's predicted grade?");
            PredictedGrade = Convert.ToChar(Console.ReadLine()[0]);
            Console.WriteLine("What is this student's accommodation?");
            Accommodation = Console.ReadLine();
        }
        public Undergraduate(string[] SplitLine, ref List<Person> People) : base(SplitLine)
        {
            string SurnameToFind = SplitLine[7];
            bool Found = false;
            foreach (Person StaffMember in People)
            {
                if (StaffMember.GetType().Name == "AcademicStaff")
                {
                    if (StaffMember.GetSurname() == SurnameToFind)
                    {
                        Tutor = (AcademicStaff)StaffMember;
                        Found = true;
                        break;
                    }

                }
            }
            if (!Found)
            {
                AcademicStaff TempTutor = new AcademicStaff(SurnameToFind);
                People.Add(TempTutor);
                Tutor = TempTutor;
            }
            PredictedGrade = Convert.ToChar(SplitLine[8]);
            Accommodation = SplitLine[9];
        }

        public override void DisplayProperties()
        {
            base.DisplayProperties();
            Console.Write($", Tutor Name: {Tutor.GetForename()} {Tutor.GetSurname()}, Predicted Grade: {PredictedGrade}, Accommodation: {Accommodation}");
        }

        public override void WriteProperties(StreamWriter File)
        {
            base.WriteProperties(File);
            File.Write($",{Tutor.GetSurname()},{PredictedGrade},{Accommodation}");
        }

        public override void Edit(string Response, ref List<Person> People)
        {
            switch (Response.ToUpper())
            {
                case "TUTOR":
                    Console.WriteLine("Please enter the surname of this student's new tutor");
                    string SurnameToFind = Console.ReadLine();
                    bool Found = false;
                    foreach (Person StaffMember in People)
                    {
                        if(StaffMember.GetType().Name == "AcademicStaff")
                        {
                            if (StaffMember.GetSurname() == SurnameToFind)
                            {
                                Console.WriteLine("Found tutor!");
                                Tutor = (AcademicStaff)StaffMember;
                                Found = true;
                                break;
                            }
                        }
                    }
                    if (!Found)
                    {
                        Console.WriteLine("Tutor not found. Creating new tutor on system.");
                        AcademicStaff TempTutor = new AcademicStaff(SurnameToFind);
                        People.Add(TempTutor);
                        Tutor = TempTutor;
                    }
                    break;
                case "PREDICTEDGRADE":
                    Console.WriteLine("Please enter new Predicted Grade");
                    PredictedGrade = Convert.ToChar(Console.ReadLine());
                    break;
                case "ACCOMMODATION":
                    Console.WriteLine("Please enter new Accommodation");
                    Accommodation = Console.ReadLine();
                    break;
                default:
                    base.Edit(Response, ref People);
                    break;
            }
        }

        public AcademicStaff GetTutor()
        {
            return Tutor;
        }
        public void SetTutor(AcademicStaff tutor)
        {
            Tutor = tutor;
        }

        public char GetPredictedGrade()
        {
            return PredictedGrade;
        }
        public void SetPredictedGrade(char grade)
        {
            PredictedGrade = grade;
        }

        public string GetAccommodation()
        {
            return Accommodation;
        }
        public void SetAccommodation(string accommodation)
        {
            Accommodation = accommodation;
        }

    } 
    
    class Postgraduate : Student
    {
        private bool ResearchStudent;
        private AcademicStaff Supervisor;
        private string DegreeLevel;

        public Postgraduate(ref List<Person> People)
        {
            SetType("Postgraduate");
            Console.WriteLine($"Is this student a Research Student? Yes / No");
            if (Console.ReadLine().ToUpper() == "YES")
            {
                ResearchStudent = true;
            }
            else
            {
                ResearchStudent = false;
            }
            Console.WriteLine("Please enter the surname of this student's supervisor");
            string SurnameToFind = Console.ReadLine();
            bool Found = false;
            foreach (Person StaffMember in People)
            {
                if (StaffMember.GetType().Name == "AcademicStaff")
                {
                    if (StaffMember.GetSurname() == SurnameToFind)
                    {
                        Console.WriteLine("Found supervisor!");
                        Supervisor = (AcademicStaff)StaffMember;
                        Found = true;
                        break;
                    }
                }
            }
            if (!Found)
            {
                Console.WriteLine("Supervisor not found. Creating new supervisor on system.");
                AcademicStaff TempSupervisor = new AcademicStaff(SurnameToFind);
                People.Add(TempSupervisor);
                Supervisor = TempSupervisor;
            }
            Console.WriteLine("Please enter this student's Degree level");
            DegreeLevel = Console.ReadLine();
        }
        public Postgraduate(string[] SplitLine, ref List<Person> People) : base(SplitLine)
        {
            ResearchStudent = Convert.ToBoolean(SplitLine[7]);
            string SurnameToFind = SplitLine[8];
            bool Found = false;
            foreach (Person StaffMember in People)
            {
                if (StaffMember.GetType().Name == "AcademicStaff")
                {
                    if (StaffMember.GetSurname() == SurnameToFind)
                    {
                        Supervisor = (AcademicStaff)StaffMember;
                        Found = true;
                        break;
                    }

                }
            }
            if (!Found)
            {
                AcademicStaff TempSupervisor = new AcademicStaff(SurnameToFind);
                People.Add(TempSupervisor);
                Supervisor = TempSupervisor;
            }
            DegreeLevel = SplitLine[9];
        }

        public override void DisplayProperties()
        {
            base.DisplayProperties();
            Console.Write($", Is a Research Student: {ResearchStudent}, Supervisor Name: {Supervisor.GetForename()} {Supervisor.GetSurname()} Degree Level: {DegreeLevel}");
        }

        public override void WriteProperties(StreamWriter File)
        {
            base.WriteProperties(File);
            File.Write($",{ResearchStudent},{Supervisor.GetSurname()},{DegreeLevel}");
        }

        public override void Edit(string Response, ref List<Person> People)
        {
            switch (Response.ToUpper())
            {
                case "SUPERVISOR":
                    Console.WriteLine("Please enter the surname of this student's new supervisor");
                    string SurnameToFind = Console.ReadLine();
                    bool Found = false;
                    foreach (Person StaffMember in People)
                    {
                        if(StaffMember.GetType().Name == "AcademicStaff")
                        {
                            if (StaffMember.GetSurname() == SurnameToFind)
                            {
                                Console.WriteLine("Found supervisor!");
                                Supervisor = (AcademicStaff)StaffMember;
                                Found = true;
                                break;
                            }
                        }
                    }
                    if (!Found)
                    {
                        Console.WriteLine("Tutor not found. Creating new supervisor on system.");
                        AcademicStaff TempSupervisor = new AcademicStaff(SurnameToFind);
                        People.Add(TempSupervisor);
                        Supervisor = TempSupervisor;
                    }
                    break;
                case "RESEARCHSTUDENT":
                    Console.WriteLine("Is this student a research student?");
                    if (Console.ReadLine().ToUpper() == "YES") {
                        ResearchStudent = true;
                    } else {
                        ResearchStudent = false;
                    }
                    break;
                case "DEGREELEVEL":
                    Console.WriteLine("Please enter new Degree Level");
                    DegreeLevel = Console.ReadLine();
                    break;
                default:
                    base.Edit(Response, ref People);
                    break;
            }
        }

        public bool IsResearchStudent()
        {
            return ResearchStudent;
        }
        public void SetResearchStudent(bool research)
        {
            ResearchStudent = research;
        }

        public AcademicStaff GetSupervisor()
        {
            return Supervisor;
        }
        public void SetSupervisor(AcademicStaff supervisor)
        {
            Supervisor = supervisor;
        }

        public string GetDegreeLevel()
        {
            return DegreeLevel;
        }
        public void SetDegreeLevel(string level)
        {
            DegreeLevel = level;
        }

    }
    
    class Candidate : Student
    {
        private List<Qualification> Qualifications = new List<Qualification>();
        private string CurrentSchool;
        private DateTime DateApplied;

        public Candidate()
        {
            SetType("Candidate");
            Console.WriteLine("How many qualifications to enter for this student?");
            int Number = Convert.ToInt32(Console.ReadLine());
            for(int i = 0; i < Number; i++)
            {
                Qualifications.Add(new Qualification());
            }
            Console.WriteLine("Please enter candidate's current educational establishment");
            CurrentSchool = Console.ReadLine();
            DateApplied = DateTime.Now;
        }
        public Candidate(string[] SplitLine) : base(SplitLine)
        {
            SplitLine[7] = SplitLine[7].Substring(0, SplitLine[7].Length - 1);
            string[] QualificationArray = SplitLine[7].Split(';');
            foreach (string s in QualificationArray)
            {
                Qualifications.Add(new Qualification(s));
            }
            CurrentSchool = SplitLine[8];
            DateApplied = Convert.ToDateTime(SplitLine[9]);
        }

        public override void DisplayProperties()
        {
            base.DisplayProperties();
            Console.Write(", Qualifications: ");
            foreach (Qualification Q in Qualifications)
            {
                Console.Write($"{Q.GetSubject()}: {Q.GetGrade()}; ");
            }
            Console.Write($", Current School: {CurrentSchool} Date Applied: {DateApplied}");
        }

        public override void WriteProperties(StreamWriter File)
        {
            base.WriteProperties(File);
            File.Write(",");
            foreach(Qualification Q in Qualifications)
            {
                File.Write($"{Q.GetSubject()}:{Q.GetGrade()};");
            }
            File.Write($",{CurrentSchool},{DateApplied}");
        }

        public override void Edit(string Response, ref List<Person> People)
        {
            switch (Response.ToUpper())
            {
                case "QUALIFICATIONS":
                    Console.WriteLine("Do you wish to keep current qualifications");
                    if(Console.ReadLine().ToUpper() == "YES")
                    {
                        Console.WriteLine("Current qualifications for student:");
                        foreach(Qualification q in Qualifications)
                        {
                            Console.WriteLine($"Subject: {q.GetSubject()} Grade: {q.GetGrade()}");
                        }
                        Console.WriteLine("How many new qualifications to enter for this student?");
                        int Number = Convert.ToInt32(Console.ReadLine());
                        for (int i = 0; i < Number; i++)
                        {
                            Qualifications.Add(new Qualification());
                        }
                    } else {
                        Qualifications = new List<Qualification>();
                        Console.WriteLine("How many qualifications to enter for this student?");
                        int Number = Convert.ToInt32(Console.ReadLine());
                        for (int i = 0; i < Number; i++)
                        {
                            Qualifications.Add(new Qualification());
                        }
                    }
                    break;
                case "CURRENTSCHOOL":
                    Console.WriteLine("Please enter new Current School");
                    CurrentSchool = Console.ReadLine();
                    break;
                case "DATEAPPLIED":
                    Console.WriteLine("Please enter new Date Applied in the format DD/MM/YYYY");
                    DateApplied = Convert.ToDateTime(Console.ReadLine());
                    break;
                default:
                    base.Edit(Response, ref People);
                    break;
            }
        }

        public List<Qualification> GetQualifications()
        {
            return Qualifications;
        }
        public void SetQualifications(List<Qualification> qualifications)
        {
            Qualifications = qualifications;
        }

        public string GetCurrentSchool()
        {
            return CurrentSchool;
        }
        public void SetCurrentSchool(string school)
        {
            CurrentSchool = school;
        }

        public DateTime GetDateApplied()
        {
            return DateApplied;
        }
        public void SetDateApplied(DateTime date)
        {
            DateApplied = date;
        }

    } 

    
    class External : Person
    {
        private string Occupation;

        public External()
        {
            SetType("External");
            Console.WriteLine("What is your occupation");
            Occupation = Console.ReadLine();
        }
        public External(string[] SplitLine) : base(SplitLine)
        {
            Occupation = SplitLine[4];
        }

        public override void DisplayProperties()
        {
            base.DisplayProperties();
            Console.Write($", Occupation: {Occupation}");
        }

        public override void WriteProperties(StreamWriter File)
        {
            base.WriteProperties(File);
            File.Write($",{Occupation}");
        }

        public override void Edit(string Response, ref List<Person> People)
        {
            switch (Response.ToUpper())
            {
                case "OCCUPATION":
                    Console.WriteLine("Please enter new Occupation");
                    Occupation = Console.ReadLine();
                    break;
                default:
                    base.Edit(Response, ref People);
                    break;
            }
        }

        public string GetOccupation()
        {
            return Occupation;
        }
        public void SetOccupation(string occupation)
        {
            Occupation = occupation;
        }
    } 

    class ServiceProvider : External
    {
        private string Role;
        private bool Permanent;

        public ServiceProvider()
        {
            SetType("ServiceProvider");
            Console.WriteLine("What is your role as a service provider");
            Role = Console.ReadLine();
            Console.WriteLine("Is this a permanent provider? Yes / No");
            if (Console.ReadLine().ToUpper() == "YES")
            {
                Permanent = true;
            }
            else
            {
                Permanent = false;
            }
        }
        public ServiceProvider(string[] SplitLine) : base(SplitLine)
        {
            Role = SplitLine[5];
            Permanent = Convert.ToBoolean(SplitLine[6]);
        }

        public override void DisplayProperties()
        {
            base.DisplayProperties();
            Console.Write($", Role: {Role}, Is Permanent: {Permanent}");
        }

        public override void WriteProperties(StreamWriter File)
        {
            base.WriteProperties(File);
            File.Write($",{Role},{Permanent}");
        }

        public override void Edit(string Response, ref List<Person> People)
        {
            switch (Response.ToUpper())
            {
                case "ROLE":
                    Console.WriteLine("Please enter new Role");
                    Role = Console.ReadLine();
                    break;
                case "PERMANENT":
                    Console.WriteLine("Is this Service Provider permanent?");
                    if(Console.ReadLine().ToUpper() == "YES")
                    {
                        Permanent = true;
                    } else {
                        Permanent = false;
                    }
                    break;
                default:
                    base.Edit(Response, ref People);
                    break;
            }
        }

        public string GetRole()
        {
            return Role;
        }
        public void SetRole(string role)
        {
            Role = role;
        }

        public bool IsPermanent()
        {
            return Permanent;
        }
        public void SetPermanent(bool permanent)
        {
            Permanent = permanent;
        }
    }

    class Other : External
    {
        private string Purpose;

        public Other()
        {
            SetType("Other");
            Console.WriteLine("What is your purpose for involvement with this educational establishment?");
            Purpose = Console.ReadLine();
        }
        public Other(string[] SplitLine) : base(SplitLine)
        {
            Purpose = SplitLine[5];
        }

        public override void DisplayProperties()
        {
            base.DisplayProperties();
            Console.Write($", Purpose: {Purpose}");
        }

        public override void WriteProperties(StreamWriter File)
        {
            base.WriteProperties(File);
            File.Write($",{Purpose}");
        }

        public override void Edit(string Response, ref List<Person> People)
        {
            switch (Response.ToUpper())
            {
                case "PURPOSE":
                    Console.WriteLine("Please enter new Purpose");
                    Purpose = Console.ReadLine();
                    break;
                default:
                    base.Edit(Response, ref People);
                    break;
            }
        }

        public string GetPurpose()
        {
            return Purpose;
        }
        public void SetPurpose(string purpose)
        {
            Purpose = purpose;
        }
    }
    
    class JobApplicant : External
    {
        private List<Qualification> Qualifications = new List<Qualification>();
        private string DesiredRole;
        private bool Interviewed;
        private DateTime DateApplied;

        public JobApplicant()
        {
            SetType("JobApplicant");
            Console.WriteLine("How many qualifications to enter for this applicant?");
            int Number = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < Number; i++)
            {
                Qualifications.Add(new Qualification());
            }
            Console.WriteLine("What is the applicant's desired role");
            DesiredRole = Console.ReadLine();
            Console.WriteLine("Has this applicant been interviewed? Yes / No");
            if (Console.ReadLine().ToUpper() == "YES")
            {
                Interviewed = true;
            }
            else
            {
                Interviewed = false;
            }
            DateApplied = DateTime.Now;
        }
        public JobApplicant(string[] SplitLine) : base(SplitLine)
        {
            SplitLine[5] = SplitLine[5].Substring(0, SplitLine[5].Length - 1);
            string[] QualificationArray = SplitLine[5].Split(';');
            foreach(string s in QualificationArray)
            {
                Qualifications.Add(new Qualification(s));

            }
            DesiredRole = SplitLine[6];
            Interviewed = Convert.ToBoolean(SplitLine[7]);
            DateApplied = Convert.ToDateTime(SplitLine[8]);
        }

        public override void DisplayProperties()
        {
            base.DisplayProperties();
            Console.Write(", Qualifications:");
            foreach (Qualification Q in Qualifications)
            {
                Console.Write($"{Q.GetSubject()}: {Q.GetGrade()}; ");
            }
            Console.Write($", Desired Role: {DesiredRole}, Has been Interviewed: {Interviewed}, Date Applied: {DateApplied}");
        }

        public override void WriteProperties(StreamWriter File)
        {
            base.WriteProperties(File);
            File.Write(",");
            foreach (Qualification Q in Qualifications)
            {
                File.Write($"{Q.GetSubject()}:{Q.GetGrade()};");
            }
            File.Write($",{DesiredRole},{Interviewed}, {DateApplied}");
        }

        public override void Edit(string Response, ref List<Person> People)
        {
            switch (Response.ToUpper())
            {
                case "QUALIFICATIONS":
                    Console.WriteLine("Do you wish to keep current qualifications");
                    if (Console.ReadLine().ToUpper() == "YES")
                    {
                        Console.WriteLine("Current qualifications for applicant:");
                        foreach (Qualification q in Qualifications)
                        {
                            Console.WriteLine($"Subject: {q.GetSubject()} Grade: {q.GetGrade()}");
                        }
                        Console.WriteLine("How many new qualifications to enter for this applicant?");
                        int Number = Convert.ToInt32(Console.ReadLine());
                        for (int i = 0; i < Number; i++)
                        {
                            Qualifications.Add(new Qualification());
                        }
                    }
                    else
                    {
                        Qualifications = new List<Qualification>();
                        Console.WriteLine("How many qualifications to enter for this applicant?");
                        int Number = Convert.ToInt32(Console.ReadLine());
                        for (int i = 0; i < Number; i++)
                        {
                            Qualifications.Add(new Qualification());
                        }
                    }
                    break;
                case "DESIREDROLE":
                    Console.WriteLine("Please enter new Desired Role");
                    DesiredRole = Console.ReadLine();
                    break;
                case "INTERVIEWED":
                    Console.WriteLine("Has this applicant been interviewed?");
                    if (Console.ReadLine().ToUpper() == "YES")
                    {
                        Interviewed = true;
                    } else {
                        Interviewed = false;
                    }
                    break;
                case "DATEAPPLIED":
                    Console.WriteLine("Please enter new Date Applied in the format DD/MM/YYYY");
                    DateApplied = Convert.ToDateTime(Console.ReadLine());
                    break;
                default:
                    base.Edit(Response, ref People);
                    break;
            }
        }

        public List<Qualification> GetQualifications()
        {
            return Qualifications;
        }
        public void SetQualifications(List<Qualification> qualifications)
        {
            Qualifications = qualifications;
        }

        public string GetDesiredRole()
        {
            return DesiredRole;
        }
        public void SetDesiredRole(string role)
        {
            DesiredRole = role;
        }

        public bool IsInterviewed()
        {
            return Interviewed;
        }
        public void SetInterviewed(bool interviewed)
        {
            Interviewed = interviewed;
        }

    }


    class Qualification
    {
        private string Subject;
        private char Grade;

        public Qualification()
        {
            Console.WriteLine("Enter Subject");
            Subject = Console.ReadLine();
            Console.WriteLine("Enter Grade");
            Grade = Convert.ToChar(Console.ReadLine()[0]);
        }
        public Qualification(string Qualification)
        {
            string[] SplitQualification = Qualification.Split(':');
            Subject = SplitQualification[0];
            Grade = SplitQualification[1][0];
        }

        public string GetSubject()
        {
            return Subject;
        }
        public void SetSubject(string subject)
        {
            Subject = subject;
        }

        public char GetGrade()
        {
            return Grade;
        }
        public void SetGrade(char grade)
        {
            Grade = grade;
        }
    }

    //sort out incomplete person stuff
    class MainClass
    {
        //add list sorting functionality
        public static void Main(string[] args)
        {
            List<Person> People = new List<Person>();
            bool Running = true;
            Initialise(ref People);

            while (Running)
            {
                Console.Clear();
                Console.WriteLine("-- MENU -- ");
                Console.WriteLine("1 Add new person");
                Console.WriteLine("2 View all people");
                Console.WriteLine("3 View specific person");
                Console.WriteLine("4 Save people to file");
                Console.WriteLine("5 Edit person");
                Console.WriteLine("6 Delete person");
                Console.WriteLine("7 View notifications");
                Console.WriteLine("8 Quit");

                switch (Console.ReadLine()[0])
                {
                    case '1':
                        NewPerson(ref People);
                        break;
                    case '2':
                        DisplayPeople(People);
                        break;
                    case '3':
                        DisplayPerson(People);
                        break;
                    case '4':
                        SaveFile(ref People);
                        break;
                    case '5':
                        EditPerson(ref People);
                        break;
                    case '6':
                        DeletePerson(ref People);
                        break;
                    case '7':
                        ViewNotifications(ref People);
                        break;
                    case '8':
                        Running = false;
                        break;
                    default:
                        Console.WriteLine("Option unavailable. Try again");
                        break;
                }
            }
            Console.WriteLine("Would you like to save to disk?");
            string Response = Console.ReadLine();
            if (Response.ToUpper() == "YES")
            {
                SaveFile(ref People);
            }
            Console.WriteLine("Thank you for using this software. Have a good day");
        }
        
        public static void Initialise(ref List<Person> People)
        {
            string Filename = "Data file.txt";
            try
            {
                using (StreamReader Reader = new StreamReader(Filename))
                {
                    while (!Reader.EndOfStream)
                    {
                        string CurrentLine = Reader.ReadLine();
                        string[] SplitLine = CurrentLine.Split(',');
                        try
                        {
                            Person TempPerson = new Person(SplitLine);
                            switch (SplitLine[0])
                            {
                                case "Person":
                                    TempPerson = new Person(SplitLine);
                                    break;
                                case "Staff":
                                    TempPerson = new Staff(SplitLine);
                                    break;
                                case "AcademicStaff":
                                    TempPerson = new AcademicStaff(SplitLine);
                                    break;
                                case "SupportStaff":
                                    TempPerson = new SupportStaff(SplitLine);
                                    break;
                                case "Student":
                                    TempPerson = new Student(SplitLine);
                                    break;
                                case "Undergraduate":
                                    TempPerson = new Undergraduate(SplitLine, ref People);
                                    break;
                                case "Postgraduate":
                                    TempPerson = new Postgraduate(SplitLine, ref People);
                                    break;
                                case "Candidate":
                                    TempPerson = new Candidate(SplitLine);
                                    break;
                                case "External":
                                    TempPerson = new External(SplitLine);
                                    break;
                                case "ServiceProvider":
                                    TempPerson = new ServiceProvider(SplitLine);
                                    break;
                                case "Other":
                                    TempPerson = new Other(SplitLine);
                                    break;
                                case "JobApplicant":
                                    TempPerson = new JobApplicant(SplitLine);
                                    break;
                                default:
                                    Console.WriteLine("Unknown person type");
                                    break;
                            }
                            Console.WriteLine("Person added");
                            People.Add(TempPerson);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("error entering person");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error found upon initialisation");
                Console.WriteLine(ex);
            }
            
        }
        
        public static void NewPerson(ref List<Person> People)
        {
            Console.WriteLine("-- ADD PERSON --");
            Console.WriteLine("1 Person");
            Console.WriteLine("2 Staff");
            Console.WriteLine("3 Student");
            Console.WriteLine("4 External");

            switch (Console.ReadLine()[0])
            {
                case '1':
                    People.Add(new Person());
                    break;
                case '2':
                    Console.WriteLine("1 Staff");
                    Console.WriteLine("2 Academic Staff");
                    Console.WriteLine("3 Support Staff");
                    switch (Console.ReadLine()[0])
                    {
                        case '1':
                            People.Add(new Staff());
                            break;
                        case '2':
                            People.Add(new AcademicStaff());
                            break;
                        case '3':
                            People.Add(new SupportStaff());
                            break;
                        default:
                            Console.WriteLine("Unacceptable input");
                            break;
                    }
                    break;
                case '3':
                    Console.WriteLine("1 Student");
                    Console.WriteLine("2 Undergraduate");
                    Console.WriteLine("3 Postgraduate");
                    Console.WriteLine("4 Candidate");
                    switch (Console.ReadLine()[0])
                    {
                        case '1':
                            People.Add(new Student());
                            break;
                        case '2':
                            People.Add(new Undergraduate(ref People));
                            break;
                        case '3':
                            People.Add(new Postgraduate(ref People));
                            break;
                        case '4':
                            People.Add(new Candidate());
                            break;
                        default:
                            Console.WriteLine("Unacceptable input");
                            break;
                    }
                    break;
                case '4':
                    Console.WriteLine("1 External");
                    Console.WriteLine("2 Service Provider");
                    Console.WriteLine("3 Other");
                    Console.WriteLine("4 Job Applicant");
                    switch (Console.ReadLine()[0])
                    {
                        case '1':
                            People.Add(new External());
                            break;
                        case '2':
                            People.Add(new ServiceProvider());
                            break;
                        case '3':
                            People.Add(new Other());
                            break;
                        case '4':
                            People.Add(new JobApplicant());
                            break;
                        default:
                            Console.WriteLine("Unacceptable input");
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Unacceptable input");
                    break;
            }
            Console.WriteLine($"{People[People.Count-1].GetForename()} {People[People.Count-1].GetSurname()} Added!");
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
        }
        
        public static void DisplayPeople(List<Person> People)
        {
            foreach(Person Person in People)
            {
                Person.DisplayProperties();
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
        }
        
        public static void DisplayPerson(List<Person> People)
        {
            Console.WriteLine("Enter surname of person to display");
            string Surname = Console.ReadLine();
            foreach (Person Person in People)
            {
                if (Person.GetSurname() == Surname)
                {
                    Person.DisplayProperties();
                }
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
        }
        
        public static void SaveFile(ref List<Person> People)
        {
            string Filename = "Data File.txt";
            using (StreamWriter Writer = new StreamWriter(Filename))
            {
                foreach(Person Person in People)
                {
                    Person.WriteProperties(Writer);
                    if (Person.IsIncomplete())
                    {
                        Writer.Write(",true,");
                        foreach(string s in Person.GetIncompleteProperties())
                        {
                            Console.WriteLine(s);
                            Writer.Write(s + ';');
                        }
                    }
                    Writer.WriteLine();
                }
            }
            Console.WriteLine("Write complete!");
            Console.WriteLine();
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
        }
        
        public static void EditPerson(ref List<Person> People)
        {
            Console.WriteLine("Enter surname of person to edit");
            string Surname = Console.ReadLine();
            foreach (Person Person in People)
            {
                if (Person.GetSurname().ToUpper() == Surname.ToUpper())
                {
                    Console.WriteLine($"{Person.GetForename()} {Person.GetSurname()}, {Person.GetType()}");
                    Console.WriteLine("Is this the person to edit?");
                    string Response = Console.ReadLine();
                    if(Response.ToUpper() == "YES")
                    {
                        Person.DisplayProperties();
                        Console.WriteLine("Which field do you want to edit? Enter it without spaces.");
                        Response = Console.ReadLine();
                        Console.WriteLine();
                        Person.Edit(Response, ref People);
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
        }
        
        public static void DeletePerson(ref List<Person> People)
        {
            Console.WriteLine("Enter surname of person to delete");
            string Surname = Console.ReadLine();
            List<Person> RemoveList = new List<Person>();
            foreach (Person Person in People)
            {
                if (Person.GetSurname() == Surname)
                {
                    Console.WriteLine($"Are you sure you want to delete {Person.GetForename()} {Person.GetSurname()}?");
                    Console.WriteLine("There may be more than one person with this name.");
                    Console.WriteLine($"They are a {Person.GetType().Name}.");
                    string Response = Console.ReadLine();
                    if(Response.ToUpper() == "YES")
                    {
                        RemoveList.Add(Person);
                    }
                }
            }
            foreach(Person Person in RemoveList)
            {
                People.Remove(Person);
                Console.WriteLine($"{Person.GetForename()} {Person.GetSurname()} Removed!");
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
        }

        public static void ViewNotifications(ref List<Person> People)
        {
            bool NoNotifs = true;
            List<Person> IncompletePeople = new List<Person>();
            foreach(Person P in People)
            {
                if (P.IsIncomplete())
                {
                    IncompletePeople.Add(P);
                    NoNotifs = false;
                }
            }
            foreach(Person P in IncompletePeople)
            {
                Console.WriteLine($"{P.GetType().Name}: {P.GetSurname()}");
                Console.WriteLine("Would you like to complete this person?");
                if(Console.ReadLine().ToUpper() == "YES")
                {
                    People.Remove(P);
                    P.Complete(P, ref People);
                    People.Add(P);
                }
            }
            if (NoNotifs)
            {
                Console.WriteLine("No notifications");
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
        }
    }
}