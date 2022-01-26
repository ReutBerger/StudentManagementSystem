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

## Setup Instructions:
1. Open your Chrome at [google.com](https://www.google.com/).
2. Open your DevTools Console by pressing 'F12' on keyboard(in google.com page).
3. Open the [player.js](https://github.com/ReutBerger/GLS/blob/master/GLS/wwwroot/player.js) script file.
4. Copy this file.
5. Paste it on Console and press 'Enter' on keyboard.
6. **The GLS will work.**

## Video guide
My recording explains the steps to run the solution-
