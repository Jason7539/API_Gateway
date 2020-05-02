// PASS Team successfully registers
describe("Pass team registration", () => {
  it("Input text and submit", () => {
    cy.visit("http://localhost:8080/#/RegisterTeam");

    cy.contains("Team Registration");

    // Arrange fill in all required fields
    cy.get("input[id='Username']").type("ASDASDASD");
    cy.get("input[id='Password']").type("ThisisavalidPassword");
    cy.get("input[id='RepeatPassword']").type("ThisisavalidPassword");
    cy.get("input[id='WebsiteUrl']").type(
      "https://www.youtube.com/watch?v=9gD6pWq_3RQ"
    );
    cy.get("input[id='CallbackUrl']").type(
      "https://www.youtube.com/watch?v=9gD6pWq_3RQ"
    );

    // Act
    cy.get("button[id='Submit']").click();

    // Assert creation was successful
    cy.contains("Registration was successfull:");
    cy.contains("Accept").click();

    // Check that url change to login
    cy.url().should("eq", "http://localhost:8080/#/Login");
  });
});

// FAIL Attempt to register with incomplete fields
describe("Fail team registration", () => {
  it("submit with fields missing", () => {
    // Arrange visit registration page
    cy.visit("http://localhost:8080/#/RegisterTeam");

    // ACT: click submit button
    cy.get("button[id='Submit']").click();

    // Assert creation was not successful
    cy.contains("Form is not valid or missing fields");
    cy.contains("Accept").click();

    // Check that url stayed
    cy.url().should("eq", "http://localhost:8080/#/RegisterTeam");
  });
});

// FAIL Passwords are not equal
describe("Fail team registration wrong passwords", () => {
  it("types different passwords into fields", () => {
    // Arrange visit registration page
    cy.visit("http://localhost:8080/#/RegisterTeam");

    // ACT: enter wrong passwords
    cy.get("input[id='Password']").type("ThisisavalidPasswordone");
    cy.get("input[id='RepeatPassword']").type("ThisisavalidPassword");

    // Assert error message
    cy.contains("Passwords are not equal");

    cy.get("button[id='Submit']").click();

    cy.contains("Form is not valid or missing fields");
    cy.contains("Accept").click();

    cy.url().should("eq", "http://localhost:8080/#/RegisterTeam");
  });
});

// FAIL Passwords are correct length
describe("Fail team registration wrong passwords", () => {
  it("type short passwords that don't meet requirements", () => {
    // Arrange visit registration page
    cy.visit("http://localhost:8080/#/RegisterTeam");

    // ACT: enter short passwords
    cy.get("input[id='Password']").type("123");
    cy.get("input[id='RepeatPassword']").type("123");

    // ASSERT error message
    cy.contains("Password must be greater or equal to 12");
    cy.get("button[id='Submit']").click();

    cy.contains("Form is not valid or missing fields");
    cy.contains("Accept").click();


    cy.url().should("eq", "http://localhost:8080/#/RegisterTeam");
  });
});

// FAIL Team username are not correct length
describe("Fail team registration short username", () => {
  it("type short username that don't meet requirements", () => {
    // Arrange visit registration page
    cy.visit("http://localhost:8080/#/RegisterTeam");

    // ACT: enter short username
    cy.get("input[id='Username']").type("aa");

    // Assert error message displayed
    cy.contains("Username must be greater or equal to 4 characters");
    cy.get("button[id='Submit']").click();

    cy.contains("Form is not valid or missing fields");
    cy.contains("Accept").click();


    cy.url().should("eq", "http://localhost:8080/#/RegisterTeam");
  });
});

// FAIL Website is invalid
describe("Fail team registration invalid WebsiteUrl", () => {
  it("type invalid website url", () => {
    // Arrange visit registration page
    cy.visit("http://localhost:8080/#/RegisterTeam");

    cy.get("input[id='Username']").type("ASDASDASDss");
    cy.get("input[id='Password']").type("ThisisavalidPassword");
    cy.get("input[id='RepeatPassword']").type("ThisisavalidPassword");
    cy.get("input[id='CallbackUrl']").type(
      "https://www.youtube.com/watch?v=GUpeDwiD64M"
    );

    cy.get("input[id='WebsiteUrl']").type("ssssss");

    // ACT: click submit button
    cy.get("button[id='Submit']").click();

    // Check that url change to login
    cy.url().should("eq", "http://localhost:8080/#/RegisterTeam");
  });
});

// FAIL Callback is invalid
describe("Fail team registration invalid CallbackUrl", () => {
  it("type invalid CallbackUrl url", () => {
    // Arrange visit registration page
    cy.visit("http://localhost:8080/#/RegisterTeam");

    cy.get("input[id='Username']").type("ASDASDASDss");
    cy.get("input[id='Password']").type("ThisisavalidPassword");
    cy.get("input[id='RepeatPassword']").type("ThisisavalidPassword");
    cy.get("input[id='WebsiteUrl']").type(
      "https://www.youtube.com/watch?v=GUpeDwiD64M"
    );

    cy.get("input[id='CallbackUrl']").type("ffff");

    // ACT: click submit button
    cy.get("button[id='Submit']").click();

    // Check that url change to login
    cy.url().should("eq", "http://localhost:8080/#/RegisterTeam");
  });
});
