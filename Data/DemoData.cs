using QuizMaster.Models;

namespace QuizMaster.Data;

public static class DemoData
{
    public static List<User> GetInitialUsers()
    {
        return new List<User>
        {
            new User
            {
                Id = 1,
                FullName = "System Administrator",
                Email = "admin@quizmaster.com",
                Password = "admin123",
                Role = "Admin",
                ProfileImage = "/images/avatars/admin.png",
                JoinedDate = new DateTime(2025, 1, 15),
                IsActive = true
            },
            new User
            {
                Id = 2,
                FullName = "Alex Johnson",
                Email = "student@quizmaster.com",
                Password = "student123",
                Role = "Student",
                ProfileImage = "/images/avatars/student1.png",
                JoinedDate = new DateTime(2025, 2, 10),
                IsActive = true
            },
            new User
            {
                Id = 3,
                FullName = "Sarah Connor",
                Email = "sarah.c@quizmaster.com",
                Password = "student123",
                Role = "Student",
                ProfileImage = "/images/avatars/student2.png",
                JoinedDate = new DateTime(2025, 2, 18),
                IsActive = true
            },
            new User
            {
                Id = 4,
                FullName = "David Miller",
                Email = "david.m@quizmaster.com",
                Password = "student123",
                Role = "Student",
                ProfileImage = "/images/avatars/student3.png",
                JoinedDate = new DateTime(2025, 3, 1),
                IsActive = true
            },
            new User
            {
                Id = 5,
                FullName = "Elena Rostova",
                Email = "elena.r@quizmaster.com",
                Password = "student123",
                Role = "Student",
                ProfileImage = "/images/avatars/student4.png",
                JoinedDate = new DateTime(2025, 3, 5),
                IsActive = true
            },
            new User
            {
                Id = 6,
                FullName = "Marcus Chen",
                Email = "marcus.c@quizmaster.com",
                Password = "student123",
                Role = "Student",
                ProfileImage = "/images/avatars/student5.png",
                JoinedDate = new DateTime(2025, 3, 12),
                IsActive = true
            }
        };
    }

    public static List<Quiz> GetInitialQuizzes()
    {
        return new List<Quiz>
        {
            // Quiz 1: C# Fundamentals
            new Quiz
            {
                Id = 1,
                Title = "C# Fundamentals",
                Description = "Test your core knowledge of C# syntax, object-oriented programming principles, data types, and memory management.",
                Category = "Programming",
                Difficulty = "Easy",
                DurationMinutes = 10,
                PassingPercentage = 50,
                ImageUrl = "/images/quizzes/csharp.svg",
                IsActive = true,
                CreatedDate = new DateTime(2025, 1, 10),
                Questions = new List<Question>
                {
                    new Question
                    {
                        Id = 101,
                        QuizId = 1,
                        QuestionText = "What keyword is used to define a class in C#?",
                        OptionA = "class",
                        OptionB = "object",
                        OptionC = "struct",
                        OptionD = "define",
                        CorrectAnswer = "A",
                        Explanation = "The 'class' keyword is used in C# to declare and define a reference-type class.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 102,
                        QuizId = 1,
                        QuestionText = "Which data type is a value type in C#?",
                        OptionA = "string",
                        OptionB = "int",
                        OptionC = "object",
                        OptionD = "delegate",
                        CorrectAnswer = "B",
                        Explanation = "In C#, 'int' (System.Int32) is a primitive struct value type stored on the stack.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 103,
                        QuizId = 1,
                        QuestionText = "Which access modifier makes a member accessible only within its own class?",
                        OptionA = "public",
                        OptionB = "internal",
                        OptionC = "private",
                        OptionD = "protected",
                        CorrectAnswer = "C",
                        Explanation = "'private' restricts access exclusively to code within the declaring type.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 104,
                        QuizId = 1,
                        QuestionText = "Which keyword is used to handle exceptions in C#?",
                        OptionA = "catch",
                        OptionB = "rescue",
                        OptionC = "handle",
                        OptionD = "fault",
                        CorrectAnswer = "A",
                        Explanation = "The 'try-catch' block uses the 'catch' keyword to intercept and handle exceptions.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 105,
                        QuizId = 1,
                        QuestionText = "What does the 'readonly' keyword signify on a field in C#?",
                        OptionA = "The field cannot be modified outside its assembly",
                        OptionB = "The field can only be assigned during declaration or inside a constructor",
                        OptionC = "The field is converted to a compile-time constant",
                        OptionD = "The field cannot be read by other threads",
                        CorrectAnswer = "B",
                        Explanation = "'readonly' fields can only be initialized at declaration or inside constructors of the same class.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 106,
                        QuizId = 1,
                        QuestionText = "Which interface must be implemented to support 'foreach' iteration in C#?",
                        OptionA = "ICollection",
                        OptionB = "IComparable",
                        OptionC = "IEnumerable",
                        OptionD = "IDisposable",
                        CorrectAnswer = "C",
                        Explanation = "'IEnumerable' exposes the GetEnumerator method required by the 'foreach' loop statement.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 107,
                        QuizId = 1,
                        QuestionText = "What does boxing mean in C#?",
                        OptionA = "Converting a reference type to a value type",
                        OptionB = "Converting a value type to an object reference type",
                        OptionC = "Encapsulating fields into properties",
                        OptionD = "Compressing memory in the garbage collector",
                        CorrectAnswer = "B",
                        Explanation = "Boxing is the implicit conversion of a value type to the type 'object' or an implemented interface.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 108,
                        QuizId = 1,
                        QuestionText = "Which operator is the null-coalescing operator in C#?",
                        OptionA = "??",
                        OptionB = "?.",
                        OptionC = "::",
                        OptionD = "=>",
                        CorrectAnswer = "A",
                        Explanation = "The '??' operator returns the left operand if not null; otherwise, it evaluates and returns the right operand.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 109,
                        QuizId = 1,
                        QuestionText = "Which keyword is used in a derived class to override a virtual method from a base class?",
                        OptionA = "new",
                        OptionB = "override",
                        OptionC = "virtual",
                        OptionD = "shadow",
                        CorrectAnswer = "B",
                        Explanation = "The 'override' modifier extends or modifies the abstract or virtual implementation of an inherited method.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 110,
                        QuizId = 1,
                        QuestionText = "What is the purpose of the 'using' statement with an IDisposable object?",
                        OptionA = "To import a namespace",
                        OptionB = "To ensure Dispose() is called even if an exception occurs",
                        OptionC = "To prevent garbage collection",
                        OptionD = "To execute code asynchronously",
                        CorrectAnswer = "B",
                        Explanation = "The 'using' statement wraps the object in a try-finally block that automatically invokes Dispose() upon exit.",
                        Marks = 1
                    }
                }
            },

            // Quiz 2: ASP.NET Core MVC
            new Quiz
            {
                Id = 2,
                Title = "ASP.NET Core MVC",
                Description = "Assess your expertise in routing, controllers, dependency injection, middleware pipeline, model binding, and Razor views.",
                Category = "Web Development",
                Difficulty = "Medium",
                DurationMinutes = 10,
                PassingPercentage = 50,
                ImageUrl = "/images/quizzes/aspnet.svg",
                IsActive = true,
                CreatedDate = new DateTime(2025, 1, 12),
                Questions = new List<Question>
                {
                    new Question
                    {
                        Id = 201,
                        QuizId = 2,
                        QuestionText = "Which architectural pattern does ASP.NET Core MVC implement?",
                        OptionA = "MVVM (Model-View-ViewModel)",
                        OptionB = "MVC (Model-View-Controller)",
                        OptionC = "MVP (Model-View-Presenter)",
                        OptionD = "Singleton Pattern",
                        CorrectAnswer = "B",
                        Explanation = "ASP.NET Core MVC implements Model-View-Controller to separate business logic, presentation, and user input handling.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 202,
                        QuizId = 2,
                        QuestionText = "Where are request processing middleware components configured in an ASP.NET Core application?",
                        OptionA = "In appsettings.json",
                        OptionB = "In Program.cs pipeline configuration",
                        OptionC = "Inside Controller constructors",
                        OptionD = "In web.config",
                        CorrectAnswer = "B",
                        Explanation = "The HTTP request pipeline in modern ASP.NET Core (.NET 6+) is configured directly in Program.cs using WebApplication app methods.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 203,
                        QuizId = 2,
                        QuestionText = "Which service lifetime creates a new instance once per HTTP client request?",
                        OptionA = "Transient",
                        OptionB = "Singleton",
                        OptionC = "Scoped",
                        OptionD = "Static",
                        CorrectAnswer = "C",
                        Explanation = "Scoped services (AddScoped) are created once per client request (connection) and disposed at the end of the request.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 204,
                        QuizId = 2,
                        QuestionText = "What Razor directive is used to specify the strongly-typed model for a view?",
                        OptionA = "@model",
                        OptionB = "@using",
                        OptionC = "@inject",
                        OptionD = "@inherits",
                        CorrectAnswer = "A",
                        Explanation = "The lowercase '@model' directive specifies the exact model type passed from the action method to the Razor view.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 205,
                        QuizId = 2,
                        QuestionText = "Which attribute prevents Cross-Site Request Forgery (CSRF) in MVC form submissions?",
                        OptionA = "[Authorize]",
                        OptionB = "[ValidateAntiForgeryToken]",
                        OptionC = "[HttpPost]",
                        OptionD = "[CorsEnable]",
                        CorrectAnswer = "B",
                        Explanation = "[ValidateAntiForgeryToken] verifies the hidden request verification token generated in the submitted HTML form.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 206,
                        QuizId = 2,
                        QuestionText = "What mechanism in ASP.NET Core maps incoming HTTP requests to controller action methods?",
                        OptionA = "Model Binding",
                        OptionB = "Routing",
                        OptionC = "Serialization",
                        OptionD = "Dependency Injection",
                        CorrectAnswer = "B",
                        Explanation = "Routing inspects the incoming HTTP request path and matches it against endpoint templates to invoke the target controller action.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 207,
                        QuizId = 2,
                        QuestionText = "Which collection is used to pass temporary data from one action to another through a redirect?",
                        OptionA = "ViewBag",
                        OptionB = "ViewData",
                        OptionC = "TempData",
                        OptionD = "Session",
                        CorrectAnswer = "C",
                        Explanation = "TempData is stored in session or cookies and survives across a single HTTP redirect (Post-Redirect-Get pattern).",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 208,
                        QuizId = 2,
                        QuestionText = "Which file serves as the default master layout for Razor views in MVC?",
                        OptionA = "_ViewStart.cshtml",
                        OptionB = "_ViewImports.cshtml",
                        OptionC = "_Layout.cshtml",
                        OptionD = "Index.cshtml",
                        CorrectAnswer = "C",
                        Explanation = "_Layout.cshtml provides the common HTML shell, scripts, styles, and @RenderBody() placeholder for views.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 209,
                        QuizId = 2,
                        QuestionText = "What does the ModelState.IsValid property check?",
                        OptionA = "If the database connection is alive",
                        OptionB = "If incoming submitted data satisfies all model validation attributes",
                        OptionC = "If the user is authenticated",
                        OptionD = "If the HTTP request method is POST",
                        CorrectAnswer = "B",
                        Explanation = "ModelState.IsValid checks whether model binding succeeded and all DataAnnotations validation rules were satisfied.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 210,
                        QuizId = 2,
                        QuestionText = "What Tag Helper attribute is used to bind an HTML input to a model property?",
                        OptionA = "asp-for",
                        OptionB = "asp-route",
                        OptionC = "asp-action",
                        OptionD = "asp-controller",
                        CorrectAnswer = "A",
                        Explanation = "The 'asp-for' Tag Helper extracts the property name and value, generating proper 'id', 'name', and 'value' attributes.",
                        Marks = 1
                    }
                }
            },

            // Quiz 3: HTML & CSS Essentials
            new Quiz
            {
                Id = 3,
                Title = "HTML & CSS Essentials",
                Description = "Master the fundamentals of semantic HTML5 tags, modern CSS flexbox, grid, responsive design, and CSS variables.",
                Category = "Web Development",
                Difficulty = "Easy",
                DurationMinutes = 10,
                PassingPercentage = 50,
                ImageUrl = "/images/quizzes/htmlcss.svg",
                IsActive = true,
                CreatedDate = new DateTime(2025, 1, 15),
                Questions = new List<Question>
                {
                    new Question
                    {
                        Id = 301,
                        QuizId = 3,
                        QuestionText = "Which HTML5 semantic element is intended to encapsulate a standalone, distributable piece of content?",
                        OptionA = "<section>",
                        OptionB = "<article>",
                        OptionC = "<div>",
                        OptionD = "<aside>",
                        CorrectAnswer = "B",
                        Explanation = "The <article> tag specifies independent, self-contained content such as a blog post, news article, or forum entry.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 302,
                        QuizId = 3,
                        QuestionText = "In the CSS Box Model, what is the space between the content and the border called?",
                        OptionA = "Margin",
                        OptionB = "Padding",
                        OptionC = "Outline",
                        OptionD = "Gap",
                        CorrectAnswer = "B",
                        Explanation = "Padding creates space inside an element between its actual content and its border.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 303,
                        QuizId = 3,
                        QuestionText = "Which CSS display property creates a 1-dimensional layout model for rows or columns?",
                        OptionA = "display: grid",
                        OptionB = "display: flex",
                        OptionC = "display: block",
                        OptionD = "display: inline",
                        CorrectAnswer = "B",
                        Explanation = "Flexbox is designed for 1-dimensional layouts (along either row or column), whereas CSS Grid is 2-dimensional.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 304,
                        QuizId = 3,
                        QuestionText = "What CSS property is used to define custom CSS variables?",
                        OptionA = "$variable-name",
                        OptionB = "--custom-name",
                        OptionC = "@var-name",
                        OptionD = "#var-name",
                        CorrectAnswer = "B",
                        Explanation = "CSS custom properties (variables) start with two hyphens, e.g. '--primary-color: #4f46e5;', and are read using var().",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 305,
                        QuizId = 3,
                        QuestionText = "Which unit in CSS is relative to the font-size of the root <html> element?",
                        OptionA = "em",
                        OptionB = "rem",
                        OptionC = "%",
                        OptionD = "vh",
                        CorrectAnswer = "B",
                        Explanation = "'rem' stands for Root EM and calculates its size relative to the font-size of the document's root element.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 306,
                        QuizId = 3,
                        QuestionText = "What meta tag in HTML is crucial for responsive mobile viewing?",
                        OptionA = "<meta charset='UTF-8'>",
                        OptionB = "<meta name='viewport' content='width=device-width, initial-scale=1.0'>",
                        OptionC = "<meta http-equiv='X-UA-Compatible'>",
                        OptionD = "<meta name='robots' content='index'>",
                        CorrectAnswer = "B",
                        Explanation = "The viewport meta tag instructs mobile browsers to render the page at the device's screen width rather than zooming out.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 307,
                        QuizId = 3,
                        QuestionText = "Which CSS property specifies how flex items are aligned along the main axis?",
                        OptionA = "align-items",
                        OptionB = "justify-content",
                        OptionC = "align-content",
                        OptionD = "flex-direction",
                        CorrectAnswer = "B",
                        Explanation = "'justify-content' aligns items along the main axis, while 'align-items' aligns them along the cross axis.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 308,
                        QuizId = 3,
                        QuestionText = "What is the CSS specificity hierarchy from highest to lowest?",
                        OptionA = "Inline styles > ID > Class/Attribute/Pseudo-class > Element",
                        OptionB = "ID > Inline styles > Class > Element",
                        OptionC = "Element > Class > ID > Inline styles",
                        OptionD = "Class > ID > Inline styles > Element",
                        CorrectAnswer = "A",
                        Explanation = "Inline styles carry the highest specificity weight (1000), followed by IDs (100), classes (10), and elements (1).",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 309,
                        QuizId = 3,
                        QuestionText = "Which HTML tag is used for the largest standard heading?",
                        OptionA = "<heading>",
                        OptionB = "<head>",
                        OptionC = "<h1>",
                        OptionD = "<h6>",
                        CorrectAnswer = "C",
                        Explanation = "<h1> defines the most important and typically largest heading in the HTML document hierarchy.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 310,
                        QuizId = 3,
                        QuestionText = "Which CSS rule is used to apply styles based on device width or screen orientation?",
                        OptionA = "@supports",
                        OptionB = "@media",
                        OptionC = "@keyframes",
                        OptionD = "@font-face",
                        CorrectAnswer = "B",
                        Explanation = "Media queries (@media rules) apply CSS rules conditionally depending on viewport width, resolution, or orientation.",
                        Marks = 1
                    }
                }
            },

            // Quiz 4: SQL Fundamentals
            new Quiz
            {
                Id = 4,
                Title = "SQL Fundamentals",
                Description = "Evaluate your understanding of relational database queries, JOIN operations, aggregations, indexing, and normalization.",
                Category = "Database",
                Difficulty = "Medium",
                DurationMinutes = 10,
                PassingPercentage = 50,
                ImageUrl = "/images/quizzes/sql.svg",
                IsActive = true,
                CreatedDate = new DateTime(2025, 1, 20),
                Questions = new List<Question>
                {
                    new Question
                    {
                        Id = 401,
                        QuizId = 4,
                        QuestionText = "Which SQL clause is used to filter records returned by a SELECT query?",
                        OptionA = "ORDER BY",
                        OptionB = "GROUP BY",
                        OptionC = "WHERE",
                        OptionD = "HAVING",
                        CorrectAnswer = "C",
                        Explanation = "The 'WHERE' clause filters rows based on a specified boolean predicate before any grouping takes place.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 402,
                        QuizId = 4,
                        QuestionText = "What is the difference between WHERE and HAVING in SQL?",
                        OptionA = "WHERE filters individual rows before grouping, while HAVING filters aggregated groups",
                        OptionB = "HAVING works on single rows, while WHERE works only with JOINs",
                        OptionC = "There is no difference; they are completely interchangeable",
                        OptionD = "WHERE can only be used with numbers",
                        CorrectAnswer = "A",
                        Explanation = "WHERE filters raw rows before aggregation, whereas HAVING filters groups created by the GROUP BY clause.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 403,
                        QuizId = 4,
                        QuestionText = "Which JOIN returns all rows from the left table and matched rows from the right table?",
                        OptionA = "INNER JOIN",
                        OptionB = "LEFT JOIN (LEFT OUTER JOIN)",
                        OptionC = "RIGHT JOIN",
                        OptionD = "FULL OUTER JOIN",
                        CorrectAnswer = "B",
                        Explanation = "LEFT JOIN guarantees every row from the left table is returned, with NULLs for unmatched right-table columns.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 404,
                        QuizId = 4,
                        QuestionText = "What does the SQL command 'DELETE FROM Users;' do if executed without a WHERE clause?",
                        OptionA = "Deletes the Users table definition and schema",
                        OptionB = "Deletes all rows inside the Users table while retaining the table structure",
                        OptionC = "Generates a syntax error",
                        OptionD = "Deletes only the first row",
                        CorrectAnswer = "B",
                        Explanation = "DELETE without a WHERE clause deletes all records from the table but preserves table structure and schema.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 405,
                        QuizId = 4,
                        QuestionText = "Which aggregate function calculates the total number of non-null values in a column?",
                        OptionA = "SUM()",
                        OptionB = "TOTAL()",
                        OptionC = "COUNT()",
                        OptionD = "AVG()",
                        CorrectAnswer = "C",
                        Explanation = "COUNT(column_name) counts all non-null entries in the specified column.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 406,
                        QuizId = 4,
                        QuestionText = "What constraint uniquely identifies each record in a database table?",
                        OptionA = "FOREIGN KEY",
                        OptionB = "PRIMARY KEY",
                        OptionC = "NOT NULL",
                        OptionD = "CHECK",
                        CorrectAnswer = "B",
                        Explanation = "The PRIMARY KEY constraint enforces unique, non-null values to identify each row uniquely.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 407,
                        QuizId = 4,
                        QuestionText = "Which command is used to remove an entire table including its schema from the database?",
                        OptionA = "DROP TABLE",
                        OptionB = "DELETE TABLE",
                        OptionC = "REMOVE TABLE",
                        OptionD = "TRUNCATE TABLE",
                        CorrectAnswer = "A",
                        Explanation = "DROP TABLE completely destroys the table definition, data, indexes, and constraints.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 408,
                        QuizId = 4,
                        QuestionText = "What does ACID stand for in the context of database transactions?",
                        OptionA = "Atomicity, Consistency, Isolation, Durability",
                        OptionB = "Accuracy, Completeness, Integrity, Dependability",
                        OptionC = "Access, Control, Index, Distribution",
                        OptionD = "Automation, Concurrency, Iteration, Data",
                        CorrectAnswer = "A",
                        Explanation = "ACID stands for Atomicity, Consistency, Isolation, and Durability - four key transaction properties.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 409,
                        QuizId = 4,
                        QuestionText = "Which operator is used to search for a specified pattern in a column with wildcards like %?",
                        OptionA = "IN",
                        OptionB = "MATCH",
                        OptionC = "LIKE",
                        OptionD = "BETWEEN",
                        CorrectAnswer = "C",
                        Explanation = "The LIKE operator searches for patterns, where '%' matches zero or more characters and '_' matches a single character.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 410,
                        QuizId = 4,
                        QuestionText = "What is the primary benefit of adding an index to a database column?",
                        OptionA = "Reduces disk storage space",
                        OptionB = "Significantly speeds up data retrieval queries (SELECT)",
                        OptionC = "Speeds up INSERT and UPDATE operations",
                        OptionD = "Enforces column encryption",
                        CorrectAnswer = "B",
                        Explanation = "Indexes provide balanced B-tree lookups that drastically accelerate read queries at the cost of slight write overhead.",
                        Marks = 1
                    }
                }
            },

            // Quiz 5: JavaScript Advanced
            new Quiz
            {
                Id = 5,
                Title = "JavaScript Advanced",
                Description = "Test your grasp on closures, prototypes, event loops, promises, async/await, and modern ECMAScript features.",
                Category = "Programming",
                Difficulty = "Hard",
                DurationMinutes = 12,
                PassingPercentage = 50,
                ImageUrl = "/images/quizzes/javascript.svg",
                IsActive = true,
                CreatedDate = new DateTime(2025, 1, 25),
                Questions = new List<Question>
                {
                    new Question
                    {
                        Id = 501,
                        QuizId = 5,
                        QuestionText = "What is a closure in JavaScript?",
                        OptionA = "A function that has access to variables from its outer (enclosing) lexical scope even after that scope has finished execution",
                        OptionB = "A method that terminates an asynchronous execution thread",
                        OptionC = "An anonymous function assigned directly to an HTML event",
                        OptionD = "A syntax error that prevents function execution",
                        CorrectAnswer = "A",
                        Explanation = "A closure bundles a function together with its surrounding lexical environment, retaining access to enclosing variables.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 502,
                        QuizId = 5,
                        QuestionText = "What does the Event Loop in JavaScript do?",
                        OptionA = "Executes synchronous code in separate background threads",
                        OptionB = "Monitors the Call Stack and the Callback/Microtask Queue, pushing tasks to the stack when it is empty",
                        OptionC = "Compiles JavaScript to WebAssembly bytecode",
                        OptionD = "Manages database connections in the browser",
                        CorrectAnswer = "B",
                        Explanation = "The Event Loop continuously checks if the Call Stack is empty and moves queued microtasks and macrotasks onto it.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 503,
                        QuizId = 5,
                        QuestionText = "What is the output of 'typeof NaN' in JavaScript?",
                        OptionA = "'undefined'",
                        OptionB = "'nan'",
                        OptionC = "'number'",
                        OptionD = "'object'",
                        CorrectAnswer = "C",
                        Explanation = "In JavaScript, NaN ('Not a Number') is formally a numeric data type as defined by the IEEE 754 floating-point standard.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 504,
                        QuizId = 5,
                        QuestionText = "Which method executes after all supplied Promises have either resolved or rejected?",
                        OptionA = "Promise.all()",
                        OptionB = "Promise.race()",
                        OptionC = "Promise.allSettled()",
                        OptionD = "Promise.any()",
                        CorrectAnswer = "C",
                        Explanation = "Promise.allSettled() returns a single promise that resolves after all input promises have settled (either fulfilled or rejected).",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 505,
                        QuizId = 5,
                        QuestionText = "What is the difference between '==' and '===' in JavaScript?",
                        OptionA = "'==' compares both type and value, while '===' compares value only",
                        OptionB = "'==' performs type coercion before comparison, whereas '===' strictly checks both value and type without coercion",
                        OptionC = "'===' can only be used with numbers",
                        OptionD = "They are identical in modern ECMAScript",
                        CorrectAnswer = "B",
                        Explanation = "The loose equality operator '==' applies type coercion, while strict equality '===' returns false if types differ.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 506,
                        QuizId = 5,
                        QuestionText = "What happens when an async function throws an error that is not caught inside it?",
                        OptionA = "The browser tab immediately freezes",
                        OptionB = "The function returns a rejected Promise",
                        OptionC = "The error is ignored and null is returned",
                        OptionD = "A syntax error is thrown at compile time",
                        CorrectAnswer = "B",
                        Explanation = "An 'async' function automatically wraps its return value in a Promise; throwing an uncaught error returns a rejected Promise.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 507,
                        QuizId = 5,
                        QuestionText = "What is the effect of 'Object.freeze(obj)' on an object?",
                        OptionA = "Prevents adding new properties, removing existing properties, or changing existing property values",
                        OptionB = "Prevents adding new properties, but allows modifying existing ones",
                        OptionC = "Deletes all properties from the object",
                        OptionD = "Converts the object to an immutable Map",
                        CorrectAnswer = "A",
                        Explanation = "Object.freeze creates a shallow-immutable object where existing properties cannot be modified, added, or deleted.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 508,
                        QuizId = 5,
                        QuestionText = "How does 'this' behave inside an ES6 arrow function?",
                        OptionA = "'this' is dynamically bound to the calling object",
                        OptionB = "'this' is lexically bound from the enclosing scope where the arrow function was defined",
                        OptionC = "'this' is always undefined",
                        OptionD = "'this' refers to the global window object in all cases",
                        CorrectAnswer = "B",
                        Explanation = "Arrow functions do not have their own 'this' binding; they inherit 'this' lexically from their enclosing context.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 509,
                        QuizId = 5,
                        QuestionText = "Which microtask has higher execution priority in Node.js / modern JS runtime?",
                        OptionA = "setTimeout callback",
                        OptionB = "process.nextTick / Promise.then callback",
                        OptionC = "setInterval callback",
                        OptionD = "setImmediate callback",
                        CorrectAnswer = "B",
                        Explanation = "Microtasks (Promises and process.nextTick) are executed immediately after the current operation and before timers/macrotasks.",
                        Marks = 1
                    },
                    new Question
                    {
                        Id = 510,
                        QuizId = 5,
                        QuestionText = "What does the JavaScript spread syntax (...) do when used with an array?",
                        OptionA = "Compresses the array into a binary string",
                        OptionB = "Expands the iterable array elements in place into function arguments or array literals",
                        OptionC = "Deletes empty slots in the array",
                        OptionD = "Sorts the array in ascending order",
                        CorrectAnswer = "B",
                        Explanation = "The spread operator expands an iterable object into individual elements where zero or more arguments or elements are expected.",
                        Marks = 1
                    }
                }
            }
        };
    }

    public static List<QuizAttempt> GetInitialAttempts()
    {
        return new List<QuizAttempt>
        {
            new QuizAttempt
            {
                Id = 1,
                UserId = 2, // Alex Johnson (student@quizmaster.com)
                QuizId = 1,
                Score = 9,
                TotalMarks = 10,
                Percentage = 90.0,
                CorrectAnswers = 9,
                WrongAnswers = 1,
                Unanswered = 0,
                Status = "Passed",
                StartedAt = DateTime.UtcNow.AddDays(-3).AddMinutes(-9),
                CompletedAt = DateTime.UtcNow.AddDays(-3),
                StudentName = "Alex Johnson",
                StudentEmail = "student@quizmaster.com",
                QuizTitle = "C# Fundamentals",
                Category = "Programming",
                Difficulty = "Easy",
                UserAnswers = new List<Answer>
                {
                    new Answer { QuestionId = 101, SelectedAnswer = "A", CorrectAnswer = "A", IsCorrect = true },
                    new Answer { QuestionId = 102, SelectedAnswer = "B", CorrectAnswer = "B", IsCorrect = true },
                    new Answer { QuestionId = 103, SelectedAnswer = "C", CorrectAnswer = "C", IsCorrect = true },
                    new Answer { QuestionId = 104, SelectedAnswer = "A", CorrectAnswer = "A", IsCorrect = true },
                    new Answer { QuestionId = 105, SelectedAnswer = "B", CorrectAnswer = "B", IsCorrect = true },
                    new Answer { QuestionId = 106, SelectedAnswer = "C", CorrectAnswer = "C", IsCorrect = true },
                    new Answer { QuestionId = 107, SelectedAnswer = "A", CorrectAnswer = "B", IsCorrect = false },
                    new Answer { QuestionId = 108, SelectedAnswer = "A", CorrectAnswer = "A", IsCorrect = true },
                    new Answer { QuestionId = 109, SelectedAnswer = "B", CorrectAnswer = "B", IsCorrect = true },
                    new Answer { QuestionId = 110, SelectedAnswer = "B", CorrectAnswer = "B", IsCorrect = true }
                }
            },
            new QuizAttempt
            {
                Id = 2,
                UserId = 2, // Alex Johnson
                QuizId = 3,
                Score = 8,
                TotalMarks = 10,
                Percentage = 80.0,
                CorrectAnswers = 8,
                WrongAnswers = 2,
                Unanswered = 0,
                Status = "Passed",
                StartedAt = DateTime.UtcNow.AddDays(-2).AddMinutes(-7),
                CompletedAt = DateTime.UtcNow.AddDays(-2),
                StudentName = "Alex Johnson",
                StudentEmail = "student@quizmaster.com",
                QuizTitle = "HTML & CSS Essentials",
                Category = "Web Development",
                Difficulty = "Easy",
                UserAnswers = new List<Answer>
                {
                    new Answer { QuestionId = 301, SelectedAnswer = "B", CorrectAnswer = "B", IsCorrect = true },
                    new Answer { QuestionId = 302, SelectedAnswer = "B", CorrectAnswer = "B", IsCorrect = true },
                    new Answer { QuestionId = 303, SelectedAnswer = "B", CorrectAnswer = "B", IsCorrect = true },
                    new Answer { QuestionId = 304, SelectedAnswer = "B", CorrectAnswer = "B", IsCorrect = true },
                    new Answer { QuestionId = 305, SelectedAnswer = "B", CorrectAnswer = "B", IsCorrect = true },
                    new Answer { QuestionId = 306, SelectedAnswer = "B", CorrectAnswer = "B", IsCorrect = true },
                    new Answer { QuestionId = 307, SelectedAnswer = "B", CorrectAnswer = "B", IsCorrect = true },
                    new Answer { QuestionId = 308, SelectedAnswer = "B", CorrectAnswer = "A", IsCorrect = false },
                    new Answer { QuestionId = 309, SelectedAnswer = "C", CorrectAnswer = "C", IsCorrect = true },
                    new Answer { QuestionId = 310, SelectedAnswer = "A", CorrectAnswer = "B", IsCorrect = false }
                }
            },
            new QuizAttempt
            {
                Id = 3,
                UserId = 3, // Sarah Connor
                QuizId = 2,
                Score = 10,
                TotalMarks = 10,
                Percentage = 100.0,
                CorrectAnswers = 10,
                WrongAnswers = 0,
                Unanswered = 0,
                Status = "Passed",
                StartedAt = DateTime.UtcNow.AddDays(-1).AddMinutes(-8),
                CompletedAt = DateTime.UtcNow.AddDays(-1),
                StudentName = "Sarah Connor",
                StudentEmail = "sarah.c@quizmaster.com",
                QuizTitle = "ASP.NET Core MVC",
                Category = "Web Development",
                Difficulty = "Medium"
            },
            new QuizAttempt
            {
                Id = 4,
                UserId = 4, // David Miller
                QuizId = 4,
                Score = 9,
                TotalMarks = 10,
                Percentage = 90.0,
                CorrectAnswers = 9,
                WrongAnswers = 1,
                Unanswered = 0,
                Status = "Passed",
                StartedAt = DateTime.UtcNow.AddDays(-1).AddMinutes(-9),
                CompletedAt = DateTime.UtcNow.AddDays(-1),
                StudentName = "David Miller",
                StudentEmail = "david.m@quizmaster.com",
                QuizTitle = "SQL Fundamentals",
                Category = "Database",
                Difficulty = "Medium"
            },
            new QuizAttempt
            {
                Id = 5,
                UserId = 5, // Elena Rostova
                QuizId = 5,
                Score = 8,
                TotalMarks = 10,
                Percentage = 80.0,
                CorrectAnswers = 8,
                WrongAnswers = 2,
                Unanswered = 0,
                Status = "Passed",
                StartedAt = DateTime.UtcNow.AddHours(-18).AddMinutes(-10),
                CompletedAt = DateTime.UtcNow.AddHours(-18),
                StudentName = "Elena Rostova",
                StudentEmail = "elena.r@quizmaster.com",
                QuizTitle = "JavaScript Advanced",
                Category = "Programming",
                Difficulty = "Hard"
            },
            new QuizAttempt
            {
                Id = 6,
                UserId = 6, // Marcus Chen
                QuizId = 2,
                Score = 7,
                TotalMarks = 10,
                Percentage = 70.0,
                CorrectAnswers = 7,
                WrongAnswers = 3,
                Unanswered = 0,
                Status = "Passed",
                StartedAt = DateTime.UtcNow.AddHours(-6).AddMinutes(-9),
                CompletedAt = DateTime.UtcNow.AddHours(-6),
                StudentName = "Marcus Chen",
                StudentEmail = "marcus.c@quizmaster.com",
                QuizTitle = "ASP.NET Core MVC",
                Category = "Web Development",
                Difficulty = "Medium"
            }
        };
    }
}
