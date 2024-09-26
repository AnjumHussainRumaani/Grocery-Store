import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { HttpClient } from '@angular/common/http';
import { AisleDetails, ProductDetails } from '../basic.model';

@Component({
  selector: 'app-tables-basic',
  templateUrl: './basic.component.html',
  styleUrls: ['./basic.component.scss']
})
export class BasicComponent implements OnInit {
  @ViewChild('productListModal') productListModal!: TemplateRef<any>;

  signupForm!: UntypedFormGroup;
  modalTitle: string | undefined;
  isEditMode: boolean = false;
  aisleData: AisleDetails[] = [];
  productsInAisle: ProductDetails[] = [];
  selectedAisle: AisleDetails | null = null;
  productData: any;

  private API_URL = 'http://localhost:5100/api';  // Base URL for your API

  constructor(
    private modalService: NgbModal,
    private fb: UntypedFormBuilder,
    private http: HttpClient  // Inject HttpClient here
  ) {}

  ngOnInit(): void {
    this.signupForm = this.fb.group({
      name: ['', Validators.required],
    });
    
    // Fetch aisles and products on initialization
    this.fetchAisles();
    this.fetchProducts();
  }

  /**
   * Fetches aisles from the API
   */
  fetchAisles(): void {
    this.http.get<AisleDetails[]>(`${this.API_URL}/aisles`).subscribe((data) => {
      this.aisleData = data;
    });
  }

  /**
   * Fetches all products from the API
   */
  fetchProducts(): void {
    this.http.get<ProductDetails[]>(`${this.API_URL}/products`).subscribe((data) => {
      this.productData = data;
    });
  }

  /**
   * Fetch products for a specific aisle
   * @param aisleId The id of the aisle to filter products for
   */
  getProductsForAisle(aisleId: string): void {
    this.http.get<ProductDetails[]>(`${this.API_URL}/aisles/${aisleId}/products`).subscribe((data) => {
      this.productsInAisle = data;
      this.openProductListModal();
    });
  }

  /**
   * Opens the modal for adding an aisle
   */
  openAddAisleModal(content: TemplateRef<NgbModal>): void {
    this.isEditMode = false;
    this.modalTitle = "Add Aisle";
    this.signupForm.reset(); // Reset the form for a new entry
    this.modalService.open(content);
  }

  /**
   * Opens the modal for editing an aisle
   * @param aisle The aisle to be edited
   */
  openEditAisleModal(aisle: AisleDetails, content: TemplateRef<NgbModal>): void {
    this.isEditMode = true;
    this.modalTitle = "Edit Aisle";
    this.selectedAisle = aisle;
    this.signupForm.patchValue({
      name: aisle.name,
    });
    this.modalService.open(content);
  }

  /**
   * Submit the form for either adding or updating an aisle
   */
  submitAisle(): void {
    if (this.isEditMode && this.selectedAisle) {
      // Update the existing aisle
      this.http.put<AisleDetails>(`${this.API_URL}/aisles/${this.selectedAisle.id}`, {
        name: this.signupForm.value.name
      }).subscribe(() => {
        this.fetchAisles();
        this.modalService.dismissAll();
      });
    } else {
      // Add a new aisle
      this.http.post<AisleDetails>(`${this.API_URL}/aisles`, {
        name: this.signupForm.value.name
      }).subscribe(() => {
        this.fetchAisles();
        this.modalService.dismissAll();
      });
    }
  }

  /**
   * Removes an aisle
   * @param aisle The aisle to be removed
   */
  removeAisle(aisle: AisleDetails): void {
    this.http.delete<void>(`${this.API_URL}/aisles/${aisle.id}`).subscribe(() => {
      this.fetchAisles(); // Refresh the aisle list after deletion
    });
  }

  /**
  * Open the modal to display products in the selected aisle
  */
  openProductListModal(): void {
    const modalRef = this.modalService.open(this.productListModal, { size: 'lg' });
  }
}
