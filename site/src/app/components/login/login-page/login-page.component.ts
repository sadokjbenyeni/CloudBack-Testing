import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { MatDialog } from "@angular/material/dialog";
import { LoginService } from "../../../services/login.service";
import { LoginDialogComponent } from "../login-dialog/login-dialog.component";
import { MatSnackBar } from "@angular/material";

@Component({
  selector: "app-login-page",
  template: ""
})
export class LoginPageComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    public loginService: LoginService,
    private snackBar: MatSnackBar
  ) {
    this.openDialog(true);
  }

  ngOnInit() {
    this.loginService.viewterms = false;
    this.loginService.ula = false;
    let ula = localStorage.getItem("ula");
    if (ula !== null && ula !== "") {
      this.loginService.ula = ula.toString() === "true";
    }

    let user = JSON.parse(sessionStorage.getItem("user"));
    if (user !== null && user !== {}) {
      this.loginService.router.navigate(["/home"]);
    }
    this.loginService.message = "";
    let register = sessionStorage.getItem("register");
    if (register === "ok") {
      this.loginService.message = "Your account has been created";
      this.loginService.colorMessage = "alert alert-info";
    }
    this.loginService.email = "";
    this.loginService.token = "";
    this.loginService.pwd = "";
    this.loginService.password = "";
    let route = this.loginService.router.url.split("/");
    this.loginService.page = route[1];
    if (route[1] === "mdp") {
      this.loginService.token = route[2];
    }
    let currentUrl = this.router.url.split("/");

    if (currentUrl[1].includes("activate")) {
      this.activationSnack("Account successfully activated!", "Go to login");
    }
  }

  ngOnDestroy(){
    this.snackBar.dismiss();
  }

  openDialog(canClose: boolean): void {
    this.dialog.open(LoginDialogComponent, {
      panelClass: "no-padding-dialog",
      disableClose: canClose,
      data: { source: this.route.parent.url }
    });
  }

  activationSnack(message: string, action: string) {
    this.snackBar.open(message, null, { verticalPosition: "top" });
  }
}
