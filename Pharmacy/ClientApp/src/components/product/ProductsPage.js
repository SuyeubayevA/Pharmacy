import React from "react";
import Button from "@mui/material/Button";
import Tooltip from "@mui/material/Tooltip";
import CreateProductDialog from "./auxilaries/CreateProductForm";
import { connect } from "react-redux";
import { bindActionCreators } from "@reduxjs/toolkit";
import PropTypes from "prop-types";
import * as productActions from "../../redux/actions/productActions";
import BasicTable from "../common/TableMaker";
import * as types from "../../entityTypes";

class ProductPage extends React.Component {
  state = {
    create: {
      shouldBeCreated: false,
    },
    product: {
      name: "",
      description: "",
      price: 0,
      productTypeId: 0,
    },
  };

  componentDidMount() {
    const { actions, products } = this.props;

    if (products.length === 0) {
      actions.loadProducts().catch((error) => {
        alert("Load products failed: " + error);
      });
    }
  }

  handleOpen = () => {
    const create = { ...this.state.create, shouldBeCreated: true };
    this.setState({ create });
  };

  handleClose = () => {
    const create = { ...this.state.create, shouldBeCreated: false };
    this.setState({ create });
  };

  handleOnChange = (event) => {
    const name = event.target.id;
    const value = event.target.value;
    const product = { ...this.state.product, [name]: value };
    this.setState({ product });
  };

  handleSubmit = (event) => {
    event.preventDefault();
    this.props.actions.createProduct(this.state.product);
    this.handleClose();
  };

  handleDelete = (productName) => {
    const { actions } = this.props;

    actions.deleteProduct(productName);
  };

  render() {
    return (
      <React.Fragment>
        <h2>Product Page</h2>
        {this.props.productTypes.length < 0 ? (
          <Tooltip title="Should be even one Product Type exist">
            <Button disabled>Add new product</Button>
          </Tooltip>
        ) : (
          <Button onClick={this.handleOpen}>Add new product</Button>
        )}

        <CreateProductDialog
          open={this.state.create.shouldBeCreated}
          handleClose={this.handleClose}
          handleSubmit={this.handleSubmit}
          handleOnChange={this.handleOnChange}
        />
        <BasicTable
          rows={this.props.products}
          type={types.PRODUCTS}
          deleteItem={this.handleDelete}
        />
      </React.Fragment>
    );
  }
}

ProductPage.propTypes = {
  products: PropTypes.array.isRequired,
  productTypes: PropTypes.array.isRequired,
  actions: PropTypes.object.isRequired,
};

function mapStateToProps(state) {
  return {
    products: state.products,
    productTypes: state.productTypes,
  };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: {
      createProduct: bindActionCreators(productActions.createProduct, dispatch),
      loadProducts: bindActionCreators(productActions.loadProducts, dispatch),
      deleteProduct: bindActionCreators(productActions.deleteProduct, dispatch),
    },
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(ProductPage);
