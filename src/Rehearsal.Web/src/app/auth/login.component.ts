import { Auth } from "./auth.service";
import { Component, OnInit } from "@angular/core";
import { AlertService } from "../alert/alert.service";
import { Router } from "@angular/router";

@Component({
    template: '<p>Bezig met inloggen...</p>'
})
export class LoginComponent implements OnInit
{
    constructor(
        public auth: Auth,
        private alertService: AlertService,
        private router: Router)
    { }

    ngOnInit(): void {
        this.auth.login()
            .catch(err => {
                this.alertService.fail('Fout bij het inloggen', err);
                return false;
            })
            .then(_ => this.router.navigate(['/dashboard']));
    }
}
