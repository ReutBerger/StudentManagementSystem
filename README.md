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
<br>
### Project technologies:
* ASP.Net Core - for building the system.
* Local database - for storing students' data.
* Redis cache in Azure - for caching the requests.

## Setup Instructions:
### 1. Install the system
You need to clone this repo:
* Click on the green `Code` button and install the project on your computer.
* Click on the `StudentManagementSystem.sln` file.
* Run the program.

### 2. Create requests client
To send requests to the system we will use Chrome 'Advanced REST client' extension.
* Click on the [link](https://chrome.google.com/webstore/detail/advanced-rest-client/hgmloofddffdnphfgcellkdfbfbjeloo/related?hl=iw).
* Click on the blue `Open app` button.

Excellent now that you have installed the system and you have client requests, you are ready to go.

## How to use the system?
The program we ran serves as a server that will receive the REST API requests we receive from the client we created.

The client can submit the following REST API requests:
Method |API | Description
--------- | --------- | ---------
POST| `/api/student` | Add a new student
DELETE| `/api/student/{id}` | Delete this {id} student  
PUT| `/api/student/{id}` | Update this {id} existing item
GET| `/api/todoitems/{id}/{field}` | Get this {id} student detail by field

> **Note:** POST and PUT requests have a request body that contains information about the student in JSON format.
> ####  Student's JSON format example:
> {<br>
>  "student_id": "3334",<br>
>  "first_name":"Reut",<br>
>  "last_name":"Berger",<br>
>  "age": 18,<br>
>  "grades_average": 111.6,<br>
>  "school_name":"HaRo'eh high school",<br>
>  "school_address":"Golomb 6, Ramat Gan"<br>
> }<br>

### Create a request from the customer
To send a request:
1. Go to our client.
2. In the `Method` field - select a method (one of the 4 in the table above).
3. In the `Request URL` field - enter the system URL, in our case - `https://localhost:44332/{api}` <br>
When we enter in `{api}` the appropriate API for the selected method (according to the table above).
4. Click in `send` button.

#### Full POST request example:
This request will add a new student to our system. <br>
Method : `POST` <br>
Request URL : `https://localhost:44332/api/student/` <br>
With the JSON example above in the body of the request.
![image](https://user-images.githubusercontent.com/58999011/151157383-9842ad6d-b6dd-45fb-9fb9-f76aab25a440.png)
Finally the system will send you a response based on the request you sent.

### You are now welcome to send as many requests as you want to the system. Good luck!
