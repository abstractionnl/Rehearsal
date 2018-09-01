import {Component, OnInit} from "@angular/core";

import {Store} from "@ngrx/store";
import {AuthModuleState} from "../../store/auth.state";
import {Login} from "../../store/auth.actions";

@Component({
    template: '<p>Bezig met inloggen...</p>'
})
export class LoginPage implements OnInit
{
    constructor(
        private store: Store<AuthModuleState>
        //public auth: AuthService,
        //private alertService: NotificationsService,         // TODO: Remove dependency by using store state
        //private router: Router
    )
    { }

    ngOnInit(): void {
        this.store.dispatch(new Login('default'));

        /*this.auth.login('default')
            .catch(err => {
                this.alertService.fail('Fout bij het inloggen', err);
                return false;
            })
            .then(_ => this.router.navigate(['/dashboard']));*/
    }
}
