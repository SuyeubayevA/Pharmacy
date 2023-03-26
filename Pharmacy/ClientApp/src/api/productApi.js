import { handleResponse, handleError } from "./apiUtils";
import * as urls from "../apiUrl";

const baseUrl = urls.API_URL + "product";

export function getProducts() {
  return fetch(baseUrl).then(handleResponse).catch(handleError);
}
