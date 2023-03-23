import { combineReducers } from "@reduxjs/toolkit";
import products from "./productReducers";

const rootReducer = combineReducers({
  products,
});

export default rootReducer;
