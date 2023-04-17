import { handleResponse, handleError } from "./apiUtils";
import * as urls from "../apiUrl";

const baseUrl = urls.API_URL + "warehouse";

export function getWarehouses() {
  return fetch(baseUrl).then(handleResponse).catch(handleError);
}
