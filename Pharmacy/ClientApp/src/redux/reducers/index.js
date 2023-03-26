import { combineReducers } from "@reduxjs/toolkit";
import products from "./productReducers";
import productTypes from "./productTypeReducers";

const rootReducer = combineReducers({
  products,
  productTypes,
});

export default rootReducer;
