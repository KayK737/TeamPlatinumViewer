﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SARViewer.Models;



namespace SARViewer
{
    class Program
    {
        #region Main
        
        static void Main(string[] args)
        {
            StudentData Data = StudentData.DeserializeFromXML(); //Gets data's value from deserializing the XML file

            if (Data != null) //if there is a XML file...
            {
                char selection;
                 
                while(true)  
                {
                    Console.Clear();//Give initial clear screen since we're in a loop.
                    Console.WriteLine("To search for a student press any key. To exit the application press E"); //Prompts user for input
                    selection = Console.ReadKey().KeyChar; //reads input
                    Console.Clear();                       //Clears console text   
                    if (selection == 'e' || selection == 'E')
                    {
                        return;
                    }
                    displayQuery(Data);                    //Performs the DisplayQuery Method

                    //Pause to keep display
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();


                }

            }
            
        }
        #endregion
        #region Methods

        private static void displayQuery(StudentData data) //Method that will prompt user for student info
        {
            Console.WriteLine("Please enter student's first name"); 
            string studentQuery1 = Console.ReadLine();              //Reads First name input
            studentQuery1 = studentQuery1.ToUpper();                //Puts First name input in all caps so it matches XML file
            Console.WriteLine("Please enter student's last name");
            string studentQuery2 = Console.ReadLine();              //Reads Last name input
            studentQuery2 = studentQuery2.ToUpper();                //Puts Last name input in all caps so it matches XML file

            var query = from student in data.StudentDirectory      //queries the student directory data                     
                        where student.FirstName == studentQuery1 || student.LastName == studentQuery2 //Looks for First/Last name values
                        select student;  //Selects all students from student directory with that match the query

            if (query.Count() == 0)
                Console.WriteLine("No students found under the name " + studentQuery1 + studentQuery2); //Checks if there was a student under the name provided
            
            if (query.Count() > 1)
            {
                Console.WriteLine("Returned more than one student. Please select the " + 
                    "number of the student whose Student Academic Record you would like to view.");

                int k = 0;
                foreach (Student s in query)
                {
                    Console.WriteLine("{0}: {1}, {2}", k+1, s.LastName, s.FirstName);
                    k++;
                }


            }

            foreach (Student student in query)  //Lists out the matching student's info
            {
                Console.WriteLine("\n\nStudent Info:");
                Console.WriteLine(student.FirstName + " " + student.LastName + "   ID: " + student.ID);
                Console.WriteLine("");
                Console.WriteLine("Student's Course List:");
                Console.WriteLine("-------------------------");
                
                foreach (Course course in student.CoursesRegistered)  //Lists out student's courses and details
                {
                    Console.WriteLine("\t" + course.Name + ", Course ID:" + course.CourseNumber);                  
                    Console.WriteLine("\t" + "Semester: " + course.Semester);
                    Console.WriteLine("\t" + "Grade: " + course.Grade);
                    Console.WriteLine("\t" + course.Credits + " Credits");
                    Console.WriteLine("-------------------------");
                }

            }
        }

        #endregion

    }
}
