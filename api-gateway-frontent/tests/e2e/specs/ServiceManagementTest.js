// PASS Update service privacy 
describe("Pass update service privacy", () => {
    it("Input text and submit", () => {
     // Arrange go to login view type in credentials
     cy.visit("http://localhost:8080/#/login");

     cy.get("input[id='Username']").type("ASDASDASD");
     cy.get("input[id='Password']").type("ThisisavalidPassword");
 
     cy.get("button[id='Login']").click();
 
     cy.url().should("eq", "http://localhost:8080/#/");

     cy.visit("http://localhost:8080/#/ManageService");

    

     cy.get("button").select(1);
     
     cy.contains("Service successfully created");
     cy.contains("Accept").click();
    });
  });

// PASS Delete service


