import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbProgressbarModule } from '@ng-bootstrap/ng-bootstrap';

// modules
import { BasicRoutingModule } from './basic-routing.module';

// components
import { BasicComponent } from './basic.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    BasicComponent
  ],
  imports: [
    CommonModule,
    NgbProgressbarModule,
    BasicRoutingModule,
    ReactiveFormsModule
  ]
})
export class BasicModule { }
