import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbProgressbarModule } from '@ng-bootstrap/ng-bootstrap';

// modules
import { ProductRoutingModule } from './product-routing.module';

// components
import { ProductComponent } from './product.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    ProductComponent
  ],
  imports: [
    CommonModule,
    NgbProgressbarModule,
    ProductRoutingModule,    
    ReactiveFormsModule
  ]
})
export class ProductModule { }
