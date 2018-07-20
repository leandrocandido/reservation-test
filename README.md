## Ryanair Reservation Test - TravelLabs

## Intro
The airline ETLBLUE is creating a brand new API to expose its reservation mechanism to other inhouse systems as well as potentially interested third parties. 

To allow this a successful candidate must implement a simple reservation API with the next features:

## Task requirements

 1. The API should expose the following operations (see appendixes for further details):
    * [GET /Flight](AppendixI.md): used to search for available flights on a certain date between two different locations.
    * [POST /Reservation](AppendixII.md): used to create a reservation in the system
    * [GET /Reservation](AppendixI.md): used to retrieve a reservation previously made.
 2. System constraints:
    * There is a maximum of 50 bags per flight in total for all the passengers
    * Each passenger can have a maximum of 5 bags per flight.
    * There are 50 seats available per flight, numbered sequentially: “01”, “02”… “50”.
    * The API should be able to accept and return JSON and XML payloads.
 3. Every endpoint should return appropriate error messages when the operation cannot be achieved for some reason. 
 4. For storage, use in-memory collections to avoid external dependencies. Use some kind of initialization to set the data into an initial state before accepting any request.
 5. Implement appropriate test cases.

## What we are looking for?
You're allowed to add any particular framework you want and keep in mind that we are looking for clean and maintainable code which follows good programming principles.

    - Clean Code
    - SOLID Principles
    - Coding in english
    - It's important to follow the requirements
    - Perform different commits evidencing the progress.

## Bonus

Included with this solution is a separate project called `Ryanair.Reservation.Bonus`. We want you to do the code review of FlightList. 
What's wrong and how might it be fixed? Perform any changes you consider should be made and add if you have some, add comments to README_Candidate.md file.

## Submission

For the correct development of the test, it is necessary to take into account the following points:

- Create an account at **GitLab**
- Create a private project of  **GitLab**  (free)
- Fork the repository and work on the solution `Ryanair.Reservation.sln`.
- Grant permissions and access to  **@ryanairLabs**  to make the pertinent observations.
- We want to see the evolution of your code, so commits are welcome.
- Please note that the application will be executed on other machines. Make sure you do not have local references.

The solution must include a README_Candidate.md file as a sort of guide with a sequence of calls to accomplish a successful reservation that could be used during the review process. Any other consideration or explanation that the candidate wants to highlight about the design/implementation process should be also included in this file.

During development the candidate may find some ambiguities or missing specs and that is fine. Feel free to take the appropriate decisions and provide some explanations about them (README_Candidate.md).

---

Thanks for your time, we look forward to hearing from you!

Ryanair Reservation Team