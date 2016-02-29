Hi Jin and Team,

Here's writeup about what I did and the thought process I went through when I designed and implemented this Web API.  

For the data, I downloaded the static collision data from the website in csv format and imported it into a MS Sql Server database.
For the purpose of this exercise and in the interest of time, I just used the built-in Sql Server functionality to import the
data.  The built-in import function has limitations as it only imports data into one table and column type is string only. 

If time is not a factor though, the proper way to do this is to design a relational database schema to normalize this data, define
primary key and foreign key relationships and to use the correct data type for columns, etc.

For the Model, in the ASP.NET Web API app, I used ADO.NET to access the data with SQL statements, again in the interest of time.
For a real world project, ORM and stored procedures may be used.

For the Controller, I implemented two methods to support the following endpoints as per my previous conversation with Jin:
1) http://mysite.com/collision/filter[borough]=Brooklyn
2) http://mysite.com/collision/filter[zip]=11214

I used the routing attribute to specified which endpoint/url corresponds to which method call.

It seems to me that when I return an object or a collection of objects from the ASP.NET Controller, the 
Controller automatically turns the object/collection into json.  However, one of Jin's suggestion/requirement is to ahere
to the JSON API specs to the best of my abilities.  To acomplish this, I create a JsonApiObject<T> class to try comply
with the JSON API specs.

For example, the JSON API specs says:
A document MUST contain at least one of the following top-level members:
data: the document's "primary data"
errors: an array of error objects

I included both.  In the case data is successfully retrieved, the "data" member would be populated and the "errors" member would be empty.
In the case that data is not successfully retrieved, the "errors" member would be populated and "data" member would be empty.

Please note that I put the JsonApiObject<T> class together with the Controller code, but if multiple Web API apps would need to ahere
to the JSON API specs, I may separate this into an API/library so that it can be shared and utilized by multiple Web API apps.

I also use a catch all in the Controller to capture all exceptions.  In a real world project, I may have different catch clauses for 
more granular exception handling.  

One thing did come to mind when I was working on the Controller code was when the Controller method makes a call to retrieve data from
the database, the call may take a long time to return, therefore, blocking/holding up the thread.  This might cause a performance
problem, since fewer threads would then be available to handle incoming request, causing the application to be less responsive.  
Therefore, alternative AsyncController or other async implmentation may be used to avoid long database calls from blocking.  If I 
were working together with you guys, I would talk this over with you guys or this could be a pair programming effort. Your thoughts?

I think that's all I can think of right now.  Please keep in mind that this is my first time doing web development, if you have
any suggestions/corrections, please let me know :)  Thank you very much in advance!

Thanks,
Ben 


