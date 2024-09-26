import { Component, OnInit, TemplateRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { HttpClient } from '@angular/common/http'; // Import HttpClient

// Data models
import { AisleDetails, ProductDetails } from '../basic.model'; // Import your models

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})

export class ProductComponent implements OnInit {

  productForm!: UntypedFormGroup;
  modalTitle: string | undefined;
  isEditMode: boolean = false;
  productData: ProductDetails[] = [];
  aisleList: AisleDetails[] = [];
  selectedProduct: ProductDetails | null = null;

  // Base URL for API endpoints
  private baseUrl = 'http://localhost:5100/api';

  constructor(
    private modalService: NgbModal,
    private fb: UntypedFormBuilder,
    private http: HttpClient, // Inject HttpClient
  ) {}

  ngOnInit(): void {
    this._fetchProductData();
    this._fetchAisleData();

    this.productForm = this.fb.group({
      name: ['', Validators.required],
      price: [0, Validators.required],
      quantity: [0, Validators.required],
      expiryDate: ['', Validators.required],
      purchaseDate: ['', Validators.required],
      aisleId: ['', Validators.required]
    });
  }

  // Fetch product data using GET API
  _fetchProductData(): void {
    this.http.get<ProductDetails[]>(`${this.baseUrl}/products`).subscribe((data: ProductDetails[]) => {
      this.productData = data;
    });
  }

  // Fetch aisle data using GET API
  _fetchAisleData(): void {
    this.http.get<AisleDetails[]>(`${this.baseUrl}/aisles`).subscribe((data: AisleDetails[]) => {
      this.aisleList = data;
    });
  }

  // Open the modal to add a new product
  openAddProductModal(content: TemplateRef<NgbModal>): void {
    this.isEditMode = false;
    this.modalTitle = "Add Product";
    this.productForm.reset();
    this.modalService.open(content);
  }

  // Open the modal to edit an existing product
  openEditProductModal(product: ProductDetails, content: TemplateRef<NgbModal>): void {
    this.isEditMode = true;
    this.modalTitle = "Edit Product";
    this.selectedProduct = product;
    this.productForm.patchValue(product);
    this.modalService.open(content);
  }

  // Submit product form (POST for new product, PUT for update)
  submitProduct(): void {
    if (this.isEditMode && this.selectedProduct) {
      this.updateProduct(this.selectedProduct.id);
    } else {
      this.addProduct();
    }
  }

  // Add new product using POST API
  addProduct(): void {
    const newProduct: Omit<ProductDetails, 'id'> = this.productForm.value;
    this.http.post<ProductDetails>(`${this.baseUrl}/products`, newProduct).subscribe((addedProduct) => {
      this.productData.push(addedProduct);
      this.modalService.dismissAll();
    });
  }

  // Update existing product using PUT API
  updateProduct(productId: string): void {
    const updatedProduct = this.productForm.value;
    this.http.put<ProductDetails>(`${this.baseUrl}/products/${productId}`, updatedProduct).subscribe((updatedData) => {
      const index = this.productData.findIndex(p => p.id === productId);
      if (index > -1) {
        this.productData[index] = updatedData;
      }
      this._fetchProductData();
      this.modalService.dismissAll();
    });
  }

  // Delete product using DELETE API
  removeProduct(product: ProductDetails): void {
    this.http.delete(`${this.baseUrl}/products/${product.id}`).subscribe(() => {
      const index = this.productData.indexOf(product);
      if (index > -1) {
        this.productData.splice(index, 1);
      }
    });
  }
}
