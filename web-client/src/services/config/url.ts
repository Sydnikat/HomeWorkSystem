const authServicePort = 9000;
export const authServiceUrl = `http://localhost:${authServicePort}/auth`;

const hwsServicePort = 44391;
const hwsServiceBaseUrl = `https://localhost:${hwsServicePort}/api`;
export const assignmentServiceUrl = `${hwsServiceBaseUrl}/assignments`;
export const groupServiceUrl = `${hwsServiceBaseUrl}/groups`;
export const homeworkServiceUrl = `${hwsServiceBaseUrl}/homeworks`;
