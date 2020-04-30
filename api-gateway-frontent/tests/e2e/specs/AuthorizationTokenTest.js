import store from "../../../src/store/index";
import * as global from "../../../src/globalExports";

var expiredToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJTY29wZSI6IjA2M2YzY2RiLTA2NWUtNDNmYi1iM2MwLTg1ZTgzZTBhMTQ5OSIsIm5iZiI6MTU4ODA2MDA4NiwiZXhwIjoxNTg4MDYxODg2LCJpYXQiOjE1ODgwNjAwODYsImlzcyI6IlNwcmluZzIwMjBBUElHYXRld2F5IiwiYXVkIjoiQVNEQVNEQVNEIn0.HrH2SWKVqaZiHNPTTPYIySpQ1EUqtRDrwjoIgUPQNVs"

// PASS Get access token and use protected resource
describe("Call a protected resource", () => {
  it("logs in as a user and call resource with token", () => {
    // Arrange go to login view type in credentials
    cy.visit("http://localhost:8080/#/login");

    cy.get("input[id='Username']").type("ASDASDASD");
    cy.get("input[id='Password']").type("ThisisavalidPassword");

    // Log in to get token
    cy.get("button[id='Login']").click();


    // Act send request to resource
    cy.request({
      url: `${global.ApiDomainName}/api/ServiceManagement/GetOwnedServicePagination/${store.state.ClientId}`,
      auth: {
        bearer: store.state.AccessToken,
      },
    }).should((response) => {
      expect(response.status).to.eq(200); // Assert that the response is correct
    });
  });
});

// FAIL Alter access token and attempt to use resource
describe("Fail to call resource", () => {
  it("logs in as a user alter token and ateempt to call resource", () => {
    // Arrange go to login view type in credentials
    cy.visit("http://localhost:8080/#/login");

    cy.get("input[id='Username']").type("ASDASDASD");
    cy.get("input[id='Password']").type("ThisisavalidPassword");

    // Act click login button
    cy.get("button[id='Login']").click();

    // Act send request
    cy.request({
      url: `${global.ApiDomainName}/api/ServiceManagement/GetOwnedServicePagination/${store.state.ClientId}`,
      failOnStatusCode: false,
      auth: {
        bearer: store.state.AccessToken + "a",
        
      },
    }).should((response) => {
      expect(response.status).to.eq(401); // Assert that the response unauthorize
    });
  });
});

// FAIL Use expired access token
describe("Fail to call resource", () => {
  it("attempt to use resource with expired token", () => {
    // Arrange go to login view type in credentials
    cy.visit("http://localhost:8080/#/login");

    cy.get("input[id='Username']").type("ASDASDASD");
    cy.get("input[id='Password']").type("ThisisavalidPassword");

    // Act click login button
    cy.get("button[id='Login']").click();

    // Act send request
    cy.request({
      url: `${global.ApiDomainName}/api/ServiceManagement/GetOwnedServicePagination/${store.state.ClientId}`,
      failOnStatusCode: false,
      auth: {
        bearer: expiredToken,
        
      },
    }).should((response) => {
      expect(response.status).to.eq(401); // Assert that the response unauthorized
    });
  });
});

// FAIL Alter a resource access token is not authorized to do
describe("Fail to call resource", () => {
  it("attempt to access resource we don't have access to", () => {
    // Arrange go to login view type in credentials
    cy.visit("http://localhost:8080/#/login");

    cy.get("input[id='Username']").type("ASDASDASD");
    cy.get("input[id='Password']").type("ThisisavalidPassword");

    // Act click login button
    cy.get("button[id='Login']").click();

    // Act send request
    cy.request({
      method: 'PATCH',
      url: `${global.ApiDomainName}/api/ServiceManagement/UpdateServicePrivacy/asdf`,
      failOnStatusCode: false,
      auth: {
        bearer: store.state.AccessToken 
        
      },
    }).should((response) => {
      expect(response.status).to.eq(403); // Assert that the response is forbidden
    });
  });
});