export interface BaseAPIResponse {
    code: string,
    message?: string,
    data?: any
}
export const CONFIG = {
    PHONE: {
        OPERATION_OPTIONS: [
            { value: "Android" },
            { value: "IOS" }
        ],
        RAM_OPTIONS: [
            { value: "4GB" },
            { value: "6GB" },
            { value: "8GB" },
            { value: "12GB" }
        ],
        ROM_OPTIONS: [
            { value: "32GB" },
            { value: "64GB" },
            { value: "128GB" },
            { value: "256GB" },
            { value: "512GB" },
        ],
        HZ_OPTIONS: [
            { value: 60 },
            { value: 90 },
            { value: 120 },
        ],
        CPU_OPTIONS: [
            { value: "Snapdragon" },
            { value: "Apple A" },
            { value: "Mediatek Dimensity" },
            { value: "Mediatek Helio" },
            { value: "Exynos" }
        ],
    },
    MAX_PRODUCT: 100,
    AUTH: {
        USER_ACCESS_TOKEN: 'user_access_token',
        USER_REFRESH_TOKEN: 'user_refresh_token',
        ADMIN_ACCESS_TOKEN: 'admin_access_token',
        ADMIN_REFRESH_TOKEN: 'admin_refresh_token',
        USER: 'user',
        ADMIN: 'admin'
    },
    STATUS_CODE: {
        SUCCESS: 'SUCCESS',
        ERROR: 'ERROR'
    },
    PAGING_OPTION: [5, 10, 20, 50, 100],
    FORM: {
        TYPE: {
            EDIT: 'edit',
            ADD: 'add',
            VIEW: 'view'
        }
    },
    ORDER: {
        MIN_DATE: "0001-01-01T00:00:00",
        PAYMENT: {
            METHOD: {
                ONLINE: 'ONLINE',
                OFFLINE: 'COD'
            },
            STATUS: {
                UNPAID: 'UNPAID',
                PAID: 'PAID',
                CONFIRMING: 'CONFIRMING'
            }
        },
        STATUS: {
            DELIVERY: 'DELIVERY',
            PREPARED: 'PREPARED',
            TRANFERED: 'TRANFERED',
            CREATED: 'CREATED',
            CANCELED: 'CANCELED'
        }
    }
}