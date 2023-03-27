import { combineReducers } from "@reduxjs/toolkit";
import products from "./productReducers";
import productTypes from "./productTypeReducers";
import warehouses from "./warehouseReducers";
import salesInfos from "./salesInfoReducers";

const rootReducer = combineReducers({
  products,
  productTypes,
  warehouses,
  salesInfos,
});

export default rootReducer;
