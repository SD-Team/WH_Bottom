import { INavData } from '@coreui/angular';
import { Injectable } from '@angular/core';

export const navItems: INavData[] = [
  {
    name: 'Dashboard',
    url: '/dashboard',
    icon: 'icon-speedometer',
  },
  {
    name: 'Receiving Material',
    url: '/receipt/main',
    icon: 'icon-list',
  },
  {
    name: 'QR Generate',
    url: 'qr',
    icon: 'icon-frame',
    children: [
      {
        name: 'Main',
        url: '/qr/main',
        icon: 'icon-arrow-right-circle',
      },
      {
        name: 'Print',
        url: '/qr/body',
        icon: 'icon-printer',
      },
    ],
  },
  {
    name: 'Input',
    url: 'io',
    icon: 'icon-loop',
    children: [
      {
        name: 'Main',
        url: '/input/main',
        icon: 'icon-arrow-right-circle',
      },
      {
        name: 'Print Again',
        url: '/input/qrcode-again',
        icon: 'icon-printer',
      },
    ],
  },
  {
    name: 'Output',
    url: '/output/main',
    icon: 'icon-loop',
  },
  {
    name: 'History',
    url: '/transfer/history',
    icon: 'icon-speech',
  },
  {
    name: 'Rack Location',
    url: '/rack/main',
    icon: 'icon-book-open',
  },
  {
    name: 'Transfer Location',
    url: '/transfer/main',
    icon: 'icon-frame',
  },
];

@Injectable({
  providedIn: 'root'  // <- ADD THIS
})
export class NavItem {
  navItems: INavData[] = [];

  constructor() { }

  getNav() {
    this.navItems = [
      {
        name: 'Dashboard',
        url: '/dashboard',
        icon: 'icon-speedometer',
      },
    ];
    const user: any = JSON.parse(localStorage.getItem('user'));
    user.role.forEach((element) => {
      if (element === 'wmsb.ReceivingMaterial') {
        const navItem = {
          name: '1. Receiving Material',
          url: '/receipt/main',
          icon: 'icon-list',
        };
        this.navItems.push(navItem);
      }
      if (element === 'wmsb.QrGenerateMain') {
        const navItem = {
          name: '2. QR Generate',
          url: 'qr',
          icon: 'icon-frame',
          children: [
            {
              name: '2.1 Main',
              url: '/qr/main',
              icon: 'icon-arrow-right-circle',
            },
            {
              name: '2.2 Print',
              url: '/qr/body',
              icon: 'icon-printer',
            },
          ],
        };
        this.navItems.push(navItem);
      }
      if (element === 'wmsb.InputMain') {
        const navItem = {
          name: '3. Input',
          url: 'io',
          icon: 'icon-loop',
          children: [
            {
              name: '3.1 Main',
              url: '/input/main',
              icon: 'icon-arrow-right-circle',
            },
            {
              name: '3.2 QrCode Print',
              url: '/input/qrcode-again',
              icon: 'icon-printer',
            },
            {
              name: '3.3 Missing Print',
              url: '/input/missing-again',
              icon: 'icon-printer',
            }
          ],
        };
        this.navItems.push(navItem);
      }
      if (element === 'wmsb.Output') {
        const navItem = {
          name: '4. Output',
          url: '/output/main',
          icon: 'icon-loop',
        };
        this.navItems.push(navItem);
      }
      if (element === 'wmsb.InOutHistory') {
        const navItem = {
          name: '6. History',
          url: '/transfer/history',
          icon: 'icon-speech',
        };
        this.navItems.push(navItem);
      }
      if (element === 'wmsb.RackLocationMain') {
        const navItem = {
          name: '7. Rack Location',
          url: '/rack/main',
          icon: 'icon-book-open',
        };
        this.navItems.push(navItem);
      }
      if (element === 'wmsb.TransferLocationMain') {
        const navItem = {
          name: '5. Transfer Location',
          url: '/transfer/main',
          icon: 'icon-frame',
        };
        this.navItems.push(navItem);
      }
    });
    return this.navItems;
  }
}
