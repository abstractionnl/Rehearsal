import {Component, OnInit} from "@angular/core";
import {Router} from "@angular/router";

import {AuthService} from "../../services/auth.service";
import {NotificationsService} from "../../../notification/services/notifications.service";

@Component({
    template: '<p>Bezig met inloggen...</p>'
})
export class LoginPage implements OnInit
{
    constructor(
        public auth: AuthService,
        private alertService: NotificationsService,         // TODO: Remove dependency by using store state
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
