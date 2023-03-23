import * as types from "./actionTypes";

export function createProduct(product) {
  return { type: types.CREATE_PRODUCT, product };
}
