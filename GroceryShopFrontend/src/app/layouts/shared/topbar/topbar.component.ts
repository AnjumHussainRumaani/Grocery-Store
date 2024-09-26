import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';

// services
import { AuthenticationService } from 'src/app/core/service/auth.service';
import { EventService } from 'src/app/core/service/event.service';

// types
import { LayoutEventType } from 'src/app/core/constants/events';
import { User } from 'src/app/core/models/auth.models';
import { AppsItem } from '../../shared/models/apps.model';
import { Language } from '../../shared/models/language.model';
import { SearchResultItem } from '../../shared/models/search.model';

// layout constants
import { LayoutType, SideBarWidth } from '../models/layout.model';

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.scss']
})
export class TopbarComponent implements OnInit {


  @Input() hideLogo: boolean = false;
  @Input() cssClasses: string = '';
  @Input() layoutType: string = '';

  languages: Language[] = [];
  apps: AppsItem[] = [];
  selectedLanguage?: Language;
  searchResults: SearchResultItem[] = [];
  loggedInUser: User | null = null;
  topnavCollapsed: boolean = false;

  // output events
  @Output() settingsButtonClicked = new EventEmitter<boolean>();
  @Output() mobileMenuButtonClicked = new EventEmitter();
  constructor (
    private authService: AuthenticationService,
    private eventService: EventService,
    public router: Router
  ) { }

  ngOnInit(): void {
    // get supported languages
    this._fetchLanguages();
  }


  /**
   * Fetches supported languages
   */
  _fetchLanguages(): void {
    this.languages = [{
      id: 1,
      name: 'English',
      flag: 'assets/images/flags/us.jpg',
    },
    {
      id: 2,
      name: 'German',
      flag: 'assets/images/flags/germany.jpg',
    },
    {
      id: 3,
      name: 'Italian',
      flag: 'assets/images/flags/italy.jpg',
    },
    {
      id: 4,
      name: 'Spanish',
      flag: 'assets/images/flags/spain.jpg',
    },
    {
      id: 5,
      name: 'Russian',
      flag: 'assets/images/flags/russia.jpg',
    }];
    this.selectedLanguage = this.languages[0];
  }

  /**
  * Change the language
  * @param language selected language from dropdown
  */
  changeLanguage(language: Language) {
    this.selectedLanguage = language;
  }

  /**
   * Toggles the right sidebar
   */
  toggleRightSidebar() {
    this.settingsButtonClicked.emit();
  }

  /*
  * Toggle left sidebar width - condensed
  */

  toggleSidebarWidth(): void {
    switch (this.layoutType) {
      case LayoutType.LAYOUT_VERTICAL:
        if (document.body.hasAttribute('data-leftbar-compact-mode') && document.body.getAttribute('data-leftbar-compact-mode') === 'condensed') {
          // document.body.removeAttribute('data-leftbar-compact-mode');
          this.eventService.broadcast(LayoutEventType.CHANGE_LEFT_SIDEBAR_TYPE, SideBarWidth.SIDEBAR_WIDTH_FIXED);
        } else {
          // document.body.setAttribute('data-leftbar-compact-mode', 'condensed');
          this.eventService.broadcast(LayoutEventType.CHANGE_LEFT_SIDEBAR_TYPE, SideBarWidth.SIDEBAR_WIDTH_CONDENSED);
        }
        break;

      case LayoutType.LAYOUT_FULL:
        document.body.classList.toggle('hide-menu');
        document.body.classList.toggle('sidebar-enable');
    }

  }



  /**
   * Toggle the menu bar when having mobile screen
   */
  toggleMobileMenu(event: any): void {
    this.topnavCollapsed = !this.topnavCollapsed;
    event.preventDefault();
    this.mobileMenuButtonClicked.emit();
  }


  onOptionClick(event: any, route?: string): void {
    if (route) {
      if (route === '.') {
        event.preventDefault();
      } else {
        this.router.navigateByUrl(route);
      }
    }


  }

}
