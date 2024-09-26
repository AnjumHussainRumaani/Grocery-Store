import { MenuItem } from '../models/menu.model';

// menu items
export const MENU: MenuItem[] = [
    { key: 'navigation', label: 'Navigation', isTitle: true },
    { key: 'Aisle List', label: 'Aisle List', isTitle: false, icon: 'uil-layer-group', link: '/tables/aisle' },
    { key: 'Products', label: 'Products', isTitle: false, icon: 'uil-layer-group', link: '/tables/product' },
]