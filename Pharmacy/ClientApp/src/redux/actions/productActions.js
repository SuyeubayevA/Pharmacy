import * as types from "./actionTypes";
import * as productApi from "../../api/productApi";

export function createProduct(product) {
  return { type: types.CREATE_PRODUCT, product };
}

function loadProductSuccess(products) {
  return { type: types.LOAD_PRODUCTS_SUCCESS, products };
}

export function loadProducts() {
  return function (dispatch) {
    return productApi
      .getProducts()
      .then((products) => {
        dispatch(loadProductSuccess(products));
      })
      .catch((error) => {
        throw error;
      });
  };
}
