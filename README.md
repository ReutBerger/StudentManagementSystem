# Student Management System

## Description:
I developed a student management system based on the REST API interface.<br>
For each student I kept the following identifying details: first name, last name, age, grades average, school name, school address.<br>
I also used the **Id** property for each student as the **unique** key in a relational database.

The system supports the following actions for each student:
1. Adding a new student.
2. Deleting a student.
3. Editing an existing student.
4. Returns student information by specific field.

In addition the system supports caching when if the same request is sent within 10 seconds the answer will come back from the cache and<br>not from the database.
Furthermore the system only supports adding students under the age of 18.


### Project technologies:
* ASP.Net Core - for building the system.
* Local database - for storing students' data.
* Redis cache in Azure - for caching the requests.

## Setup Instructions:
### 1. Install the system - 
You need to clone this repo:
* Click the green CODE button and install the project on your computer.
* Click on the StudentManagementSystem.sln file
* Run the program.
 Excellent now you have installed the system and you are ready.

## Video guide
My recording explains the steps to run the solution-
