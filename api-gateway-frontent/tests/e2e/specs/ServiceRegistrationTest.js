// PASS Create service
describe("Pass service registration", () => {
    it("Input text and submit", () => {
     // Arrange go to login view type in credentials
     cy.visit("http://localhost:8080/#/login");

     cy.get("input[id='Username']").type("ASDASDASD");
     cy.get("input[id='Password']").type("ThisisavalidPassword");
 
     cy.get("button[id='Login']").click();
 
     cy.url().should("eq", "http://localhost:8080/#/");

     cy.visit("http://localhost:8080/#/RegisterService");

     // Type in service registration form
     cy.get("input[id='RouteToAccess']").type("HELLOWORLD");
     cy.get("input[id='Input']").type("myinput");
     cy.get("input[id='Output']").type("out put");
     cy.get("input[id='DataFormat']").type("json");
     cy.get("textarea[id='Description']").type("this is my first service registration");
     cy.get("label[for='OpenTo']").click({force:true});

     cy.contains("jasonjason").click();
     cy.contains("ASDASDASD").click();

     cy.get("input[id='RouteOne']").type("https://docs.cypress.io/api/commands/get.html#Selector");

     // Act create the service
     cy.get("button[id='Create']").click();
     
     cy.contains("Service successfully created");
     cy.contains("Accept").click();
    });
  });


// FAIL Route to service Taken
describe("Fail service registration", () => {
    it("Input text route that is taken and submit", () => {
     // Arrange go to login view type in credentials
     cy.visit("http://localhost:8080/#/login");

     cy.get("input[id='Username']").type("ASDASDASD");
     cy.get("input[id='Password']").type("ThisisavalidPassword");
 
     cy.get("button[id='Login']").click();
 
     cy.url().should("eq", "http://localhost:8080/#/");

     cy.visit("http://localhost:8080/#/RegisterService");

     // Type in service registration form
     cy.get("input[id='RouteToAccess']").type("HELLOWORLD");
     cy.get("input[id='Input']").type("myinput");
     cy.get("input[id='Output']").type("out put");
     cy.get("input[id='DataFormat']").type("json");
     cy.get("textarea[id='Description']").type("this is my first service registration");
     cy.get("label[for='OpenTo']").click({force:true});

     cy.contains("jasonjason").click();
     cy.contains("ASDASDASD").click();

     cy.get("input[id='RouteOne']").type("https://docs.cypress.io/api/commands/get.html#Selector");


     // Act create the service
     cy.get("button[id='Create']").click();
     
     cy.contains("Failed to create service");
     cy.contains("Route to access is taken.");
     cy.contains("Accept").click();
    });
  });

// FAIL Form incomplete
describe("Fail service registration", () => {
    it("submit without text iput", () => {
     // Arrange go to login view type in credentials
     cy.visit("http://localhost:8080/#/login");

     cy.get("input[id='Username']").type("ASDASDASD");
     cy.get("input[id='Password']").type("ThisisavalidPassword");
 
     cy.get("button[id='Login']").click();
 
     cy.url().should("eq", "http://localhost:8080/#/");

     cy.visit("http://localhost:8080/#/RegisterService");

     // Act create the service
     cy.get("button[id='Create']").click();
     
     cy.contains("Form Invalid");
     cy.contains("Fields are missing are incorrect");
     cy.contains("Accept").click();
    });
  });


// FAIL Action not valid https 
describe("Fail service registration", () => {
    it("Input http route and submit", () => {
     // Arrange go to login view type in credentials
     cy.visit("http://localhost:8080/#/login");

     cy.get("input[id='Username']").type("ASDASDASD");
     cy.get("input[id='Password']").type("ThisisavalidPassword");
 
     cy.get("button[id='Login']").click();
 
     cy.url().should("eq", "http://localhost:8080/#/");

     cy.visit("http://localhost:8080/#/RegisterService");

     // Type in service registration form
     cy.get("input[id='RouteToAccess']").type("ffffffffffffffff");
     cy.get("input[id='Input']").type("myinput");
     cy.get("input[id='Output']").type("out put");
     cy.get("input[id='DataFormat']").type("json");
     cy.get("textarea[id='Description']").type("this is my first service registration");
     cy.get("label[for='OpenTo']").click({force:true});

     cy.contains("jasonjason").click();
     cy.contains("ASDASDASD").click();

     cy.get("input[id='RouteOne']").type("http://docs.cypress.io/api/commands/get.html#Selector");


     // Act create the service
     cy.get("button[id='Create']").click();
     
     cy.contains("Failed to create service");
     cy.contains("One of the action url is not valid or not https.");
     cy.contains("Accept").click();
    });
  });