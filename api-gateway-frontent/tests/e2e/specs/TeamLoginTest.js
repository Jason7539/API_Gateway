
// PASS Team successfully logins
describe("Pass team login successful", () => {
  it("logs in as a user", () => {
    // Arrange go to login view type in credentials
    cy.visit("http://localhost:8080/#/login");

    cy.get("input[id='Username']").type("ASDASDASD");
    cy.get("input[id='Password']").type("ThisisavalidPassword");

    // Act click login button
    cy.get("button[id='Login']").click();

    cy.url().should("eq", "http://localhost:8080/#/");
  });
});

// FAIL Username DNE
describe("Fail team login", () => {
  it("attempts to log in with non existent", () => {
    // Arrange go to login view type in credentials
    cy.visit("http://localhost:8080/#/login");

    cy.get("input[id='Username']").type("nonexistent");
    cy.get("input[id='Password']").type("ThisisavalidPassword");

    // Act click login button
    cy.get("button[id='Login']").click();

    // Assert that an error message is displayed
    cy.contains("One of the above fields is incorrect");
  });
});

// FAIL Password incorrect
describe("Fail team login", () => {
  it("attempts to log in with incorrect password", () => {
    // Arrange go to login view type in credentials
    cy.visit("http://localhost:8080/#/login");

    cy.get("input[id='Username']").type("ASDASDASD");
    cy.get("input[id='Password']").type("ThisisavalidPasswordssss");

    // Act click login button
    cy.get("button[id='Login']").click();

    // Assert that an error message is displayed
    cy.contains("One of the above fields is incorrect");
  });
});

// FAIL Form incomplete
describe("Fail team login", () => {
    it("attempts to log in with incorrect password", () => {
      // Arrange go to login view
      cy.visit("http://localhost:8080/#/login");
  
      // Act click login button
      cy.get("button[id='Login']").click();
  
      // Assert that an error message is displayed
      cy.contains("Input is not valid or missing fields");
    });
  });