import { MatDialogRef, MatCheckboxModule } from '@angular/material';
import { LoginComponent } from './../login/login.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// Material Imports
import { MatDialogModule, MatInputModule, MatCardModule, MatProgressSpinnerModule } from '@angular/material';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  exports: [
    MatDialogModule,
    MatInputModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatCheckboxModule
  ],
})
export class MaterialModule { }
