import { handleResponse, handleError } from "./apiUtils";
import * as urls from "../apiUrl";

const baseUrl = urls.API_URL + "producttype";

export function getProductTypes() {
  return fetch(baseUrl).then(handleResponse).catch(handleError);
}
