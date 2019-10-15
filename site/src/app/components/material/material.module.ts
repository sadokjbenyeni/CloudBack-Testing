import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// Material Imports
import { MatDialogModule, MatInputModule } from '@angular/material';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  exports: [
    MatDialogModule,
    MatInputModule 
  ]
})
export class MaterialModule { }
