import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { MatDialog } from "@angular/material/dialog";
import { LoginDialogComponent } from "../login-dialog/login-dialog.component";
import { MatSnackBar } from "@angular/material";

@Component({
  template: ""
})
export class LoginPageComponent implements OnInit {
  message: string;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {
    this.openDialog(true);
  }

  ngOnInit() {

    let register = localStorage.getItem("register");
    if (register === "ok") {
      this.message = "Your account has been created";
    }
    let currentUrl = this.router.url.split("/");
    if (currentUrl[1].includes("activate")) {
      this.activationSnack("Account successfully activated!");
    }
  }

  ngOnDestroy() {
    this.snackBar.dismiss();
  }

  openDialog(canClose: boolean): void {
    this.dialog.open(LoginDialogComponent, {
      panelClass: "no-padding-dialog",
      disableClose: canClose,
      data: { source: this.route.parent.url }
    });
  }

  activationSnack(message: string) {
    this.snackBar.open(message, null, { verticalPosition: "top" });
  }
}
