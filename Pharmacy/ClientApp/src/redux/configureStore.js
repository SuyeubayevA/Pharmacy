import { configureStore } from "@reduxjs/toolkit";
import rootReducer from "./reducers";

export const store = (initialState) =>
  configureStore({
    reducer: rootReducer,
    initialState,
  });
