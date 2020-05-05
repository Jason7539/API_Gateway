// Success Condition for Service Discovery that display available services
describe("Service Display Pass for albertTesting", () => {
  it("Login as albertTesting and then display available services", () => {
    // Arrange login and go to the serviceDiscovery page
    cy.visit("http://localhost:8080/#/login");

    cy.get("input[id='Username']").type("albertTesting");
    cy.get("input[id='Password']").type("123456789123");
    cy.get("button[id='Login']").click();
    cy.url().should("eq", "http://localhost:8080/#/");

    // Act move to serviceDiscovery page
    cy.visit("http://localhost:8080/#/ServiceDiscovery");

    //Assert the these services open to albertTesting should be visible
    cy.contains("testingServiceTeam1openToone").should("be.visible");
    cy.contains("testingTeamAService1").should("be.visible");
    cy.contains("testingTeamAService1").should("be.visible");
    cy.contains("testingTeamAService2").should("be.visible");
    cy.contains("testingTeamBService1").should("be.visible");
    cy.contains("testingTeamCService1").should("be.visible");
    cy.contains("testingTeamCService2").should("be.visible");
  });
});

// Success Condition for Service Discovery filter
describe("Service filter Pass matched result showing", () => {
  it("Login as albertTesting and then display available services based on filter", () => {
    // Arrange login and go to the serviceDiscovery page
    cy.visit("http://localhost:8080/#/login");

    cy.get("input[id='Username']").type("albertTesting");
    cy.get("input[id='Password']").type("123456789123");
    cy.get("button[id='Login']").click();
    cy.url().should("eq", "http://localhost:8080/#/");

    // Act move to serviceDiscovery page and enter filter to filter data
    cy.visit("http://localhost:8080/#/ServiceDiscovery");
    cy.get("input[id='filterInput']").type("testingTeamAService1");

    //Assert the available service should be visible
    cy.contains("testingTeamAService1").should("be.visible");
  });
});

//Fail Condition for Service Discovery filter
describe("Service filter not Pass no matched result", () => {
  it("Login as albertTesting and then display available services based on filter", () => {
    // Arrange login and go to the serviceDiscovery page
    cy.visit("http://localhost:8080/#/login");

    cy.get("input[id='Username']").type("albertTesting");
    cy.get("input[id='Password']").type("123456789123");
    cy.get("button[id='Login']").click();
    cy.url().should("eq", "http://localhost:8080/#/");

    // Act move to serviceDiscovery page and enter filter to filter data
    cy.visit("http://localhost:8080/#/ServiceDiscovery");
    cy.get("input[id='filterInput']").type("ThisOneWillnotbeFound");

    //Assert a service that not exist should not be visible
    cy.contains("ThisOneWillnotbeFound").should("not.be.visible");
  });
});

//Success Condition for Service Discovery unavailable services not vivible
describe("Service Display Pass unavailable services not vivible", () => {
  it("Login as teamTestingB and then display available services", () => {
    // Arrange login and go to the serviceDiscovery page
    cy.visit("http://localhost:8080/#/login");

    cy.get("input[id='Username']").type("teamTestingB");
    cy.get("input[id='Password']").type("123456789123");
    cy.get("button[id='Login']").click();
    cy.url().should("eq", "http://localhost:8080/#/");

    // Act move to serviceDiscovery page
    cy.visit("http://localhost:8080/#/ServiceDiscovery");
    //Assert the service testingTeamAService2" is not open to testingTeamB so it should not be visible
    cy.contains("testingTeamAService2").should("not.be.visible");
  });
});

//Success Condition for Service Discovery refresh
describe("Service Display Pass refreshdata", () => {
  it("Login as albertTesting, apply filter and then click refresh", () => {
    // Arrange login and go to the serviceDiscovery page
    cy.visit("http://localhost:8080/#/login");

    cy.get("input[id='Username']").type("albertTesting");
    cy.get("input[id='Password']").type("123456789123");
    cy.get("button[id='Login']").click();
    cy.url().should("eq", "http://localhost:8080/#/");

    // Act move to serviceDiscovery page, apply filter
    cy.visit("http://localhost:8080/#/ServiceDiscovery");
    cy.get("input[id='filterInput']").type("ThisOneWillnotbeFound");
    cy.contains("Refresh Data").click();
    //Assert the filter will be clean up so all available services will be visible
    cy.contains("testingServiceTeam1openToone").should("be.visible");
    cy.contains("testingTeamAService1").should("be.visible");
    cy.contains("testingTeamAService1").should("be.visible");
    cy.contains("testingTeamAService2").should("be.visible");
    cy.contains("testingTeamBService1").should("be.visible");
    cy.contains("testingTeamCService1").should("be.visible");
    cy.contains("testingTeamCService2").should("be.visible");
  });
});

//Success Condition for Service Discovery sort
describe("Service Display Pass sort endpoint", () => {
  it("Login as albertTesting, then apply sort", () => {
    // Arrange login and go to the serviceDiscovery page
    cy.visit("http://localhost:8080/#/login");

    cy.get("input[id='Username']").type("albertTesting");
    cy.get("input[id='Password']").type("123456789123");
    cy.get("button[id='Login']").click();
    cy.url().should("eq", "http://localhost:8080/#/");

    // Act move to serviceDiscovery page, click to change the sort order
    cy.visit("http://localhost:8080/#/ServiceDiscovery");
    cy.contains("EndPoint").click(70, 10, { force: true });
    cy.contains("EndPoint").click(70, 20, { force: true });
  });
});

//Success Condition for Service Discovery expand column
describe("Service Display Pass expand column", () => {
  it("Login as albertTesting, click the expand column", () => {
    // Arrange login and go to the serviceDiscovery page
    cy.visit("http://localhost:8080/#/login");

    cy.get("input[id='Username']").type("albertTesting");
    cy.get("input[id='Password']").type("123456789123");
    cy.get("button[id='Login']").click();
    cy.url().should("eq", "http://localhost:8080/#/");

    // Act move to serviceDiscovery page, click the expand column
    cy.visit("http://localhost:8080/#/ServiceDiscovery");
    cy.get(
      ":nth-child(1) > .el-table_1_column_1 > .cell > .el-table__expand-icon"
    ).click();

    //Assert
    cy.contains("Description").should("be.visible");
  });
});

//Success Condition for display service for a new user
describe("Service Display Pass display service for new team", () => {
  it("Register a new team, create a new serivce and login to view service", () => {
    cy.visit("http://localhost:8080/#/RegisterTeam");

    cy.contains("Team Registration");

    // Arrange register, login and register for a new service
    cy.get("input[id='Username']").type("testingNewTeam");
    cy.get("input[id='Password']").type("123456789123"); //random number password to meet requirement
    cy.get("input[id='RepeatPassword']").type("123456789123");
    cy.get("input[id='WebsiteUrl']").type(
      "https://www.youtube.com/watch?v=k1zo6vHYz94"
    );
    cy.get("input[id='CallbackUrl']").type(
      "https://www.youtube.com/watch?v=k1zo6vHYz94"
    );

    cy.get("button[id='Submit']").click();

    cy.contains("Registration was successfull:");
    cy.contains("Accept").click();

    cy.visit("http://localhost:8080/#/login");

    cy.get("input[id='Username']").type("testingNewTeam");
    cy.get("input[id='Password']").type("123456789123");
    cy.get("button[id='Login']").click();
    cy.url().should("eq", "http://localhost:8080/#/");

    cy.visit("http://localhost:8080/#/RegisterService");

    //register a new service for the new team
    cy.get("input[id='RouteToAccess']").type("testingTeamNewService1");
    cy.get("input[id='Input']").type("int");
    cy.get("input[id='Output']").type("int");
    cy.get("input[id='DataFormat']").type("xml");
    cy.get("textarea[id='Description']").type(
      "Testing description for new service"
    );
    cy.get("label[for='OpenTo']").click({ force: true });

    cy.contains("testingNewTeam").click();

    cy.get("input[id='RouteOne']").type(
      "https://www.youtube.com/channel/UCElzlyMtkoXaO3kFa5HL0Xw"
    );

    //Finish create the service
    cy.get("button[id='Create']").click();

    cy.contains("Service successfully created");
    cy.contains("Accept").click();

    // Act move to serviceDiscovery page, apply filter
    cy.visit("http://localhost:8080/#/ServiceDiscovery");

    //Assert new service should be visivle but other teams's services should not be visible for this new team
    cy.contains("testingTeamNewService1").should("be.visible");
    cy.contains("testingServiceTeam1openToone").should("not.be.visible");
  });
});
