import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {HeaderComponent} from "./header/header.component";
import {AuthComponent} from "./auth/auth.component";
import { AboutComponent } from './about/about.component';
import { FavoritesMenuComponent } from './favorites-menu/favorites-menu.component';
import { WordItemComponent } from './word-item/word-item.component';

@NgModule({
  declarations: [
    AppComponent,
    AboutComponent,
    FavoritesMenuComponent,
    WordItemComponent,
  ],
    imports: [
        BrowserModule,
        HeaderComponent,
        AuthComponent
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
