import * as React from "react";
import * as types from "../../entityTypes";
import ProductTable from "./tables/ProductTable";
import ProductTypeTable from "./tables/ProductTypeTable";

export default function BasicTable({ rows, type }) {
  if (type === types.PRODUCTS) {
    return <ProductTable rows={rows} />;
  }
  if (type === types.PRODUCTTYPES) {
    return <ProductTypeTable rows={rows} />;
  }
}
