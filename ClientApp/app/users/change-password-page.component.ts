import {Component} from "@angular/core";
import {UsersService} from "./users.service";
import {Router, ActivatedRoute} from "@angular/router";
import {User} from "./user.model";

@Component({
    templateUrl: "./change-password-page.component.html",
    styleUrls: [
        "../../styles/page.css",
        "./change-password-page.component.css"
    ],
    selector: "ce-change-password-page"
})
export class ChangePasswordPageComponent {
    constructor(
        private _activatedRoute: ActivatedRoute,
        private _usersService: UsersService,
        private _router: Router
    ) { }

    ngOnInit() {
        if (this._activatedRoute.snapshot.params["id"]) {
            this._usersService.getById({ id: this._activatedRoute.snapshot.params["id"] })
                .subscribe(x => this.user = x.user);
        }
    }

    public tryToChangePassword($event) {        
        this._usersService.changePassword({
            user: this.user,
            password: $event.value.password,
            confirmPassword: $event.value.confirmPassword
        }).subscribe();
        this._router.navigate(["users"]);
    }

    public user: User = <User>{}
}
